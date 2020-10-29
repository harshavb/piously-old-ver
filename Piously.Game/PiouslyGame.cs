using System;
using System.Collections.Generic;
using System.Diagnostics;
using osu.Framework.Configuration;
using osu.Framework.Screens;
using Piously.Game.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Overlays;
using osu.Framework.Logging;
using osu.Framework.Allocation;
using Piously.Game.Overlays.Toolbar;
using Piously.Game.Screens;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu.Framework.Threading;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Input;
using Piously.Game.Input.Bindings;
using Piously.Game.Online.Chat;
using osuTK.Graphics;
using Piously.Game.Overlays.Volume;
using Piously.Game.Updater;
using Piously.Game.Utils;
using LogLevel = osu.Framework.Logging.LogLevel;
using Piously.Game.Screens.Menu;

namespace Piously.Game
{
    public class PiouslyGame : PiouslyGameBase, IKeyBindingHandler<GlobalAction>
    {
        public Toolbar Toolbar;

        private ChatOverlay chatOverlay;

        private ChannelManager channelManager;

        private DashboardOverlay dashboard;

        private UserProfileOverlay userProfile;

        protected SentryLogger SentryLogger;

        public virtual Storage GetStorageForStableInstall() => null;

        public float ToolbarOffset => (Toolbar?.Position.Y ?? 0) + (Toolbar?.DrawHeight ?? 0);

        private IdleTracker idleTracker;

        /// <summary>
        /// Whether overlays should be able to be opened game-wide. Value is sourced from the current active screen.
        /// </summary>
        public readonly IBindable<OverlayActivation> OverlayActivationMode = new Bindable<OverlayActivation>();

        /// <summary>
        /// Whether the local user is currently interacting with the game in a way that should not be interrupted.
        /// </summary>
        /// <remarks>
        /// This is exclusively managed by <see cref="Player"/>. If other components are mutating this state, a more
        /// resilient method should be used to ensure correct state.
        /// </remarks>
        public Bindable<bool> LocalUserPlaying = new BindableBool();

        protected PiouslyScreenStack ScreenStack;

        protected BackButton BackButton;

        protected SettingsOverlay Settings;

        private VolumeOverlay volume;

        private PiouslyLogo piouslyLogo;

        private MainMenu menuScreen;

        [CanBeNull]
        private IntroScreen introScreen;

        private readonly string[] args;

        private readonly List<OverlayContainer> overlays = new List<OverlayContainer>();

        private readonly List<OverlayContainer> visibleBlockingOverlays = new List<OverlayContainer>();

        private void updateBlockingOverlayFade() =>
            screenContainer.FadeColour(visibleBlockingOverlays.Any() ? PiouslyColor.Gray(0.5f) : Color4.White, 500, Easing.OutQuint);

        public void AddBlockingOverlay(OverlayContainer overlay)
        {
            if (!visibleBlockingOverlays.Contains(overlay))
                visibleBlockingOverlays.Add(overlay);
            updateBlockingOverlayFade();
        }

        public void RemoveBlockingOverlay(OverlayContainer overlay)
        {
            visibleBlockingOverlays.Remove(overlay);
            updateBlockingOverlayFade();
        }

        /// <summary>
        /// Close all game-wide overlays.
        /// </summary>
        /// <param name="hideToolbar">Whether the toolbar should also be hidden.</param>
        public void CloseAllOverlays(bool hideToolbar = true)
        {
            foreach (var overlay in overlays)
                overlay.Hide();

            if (hideToolbar) Toolbar.Hide();
        }

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance && !DebugUtils.IsDebugBuild)
            {
                Logger.Log(@"Piously does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            if (args?.Length > 0)
            {
                var paths = args.Where(a => !a.StartsWith('-')).ToArray();
                if (paths.Length > 0)
                    Task.Run(() => Import(paths));
            }

            dependencies.CacheAs(this);

            dependencies.Cache(SentryLogger);

            dependencies.Cache(piouslyLogo = new PiouslyLogo { Alpha = 0 });

            IsActive.BindValueChanged(active => updateActiveState(active.NewValue), true);

            Audio.AddAdjustment(AdjustableProperty.Volume, inactiveVolumeFade);
        }

        private ExternalLinkOpener externalLinkOpener;

        /// <summary>
        /// Handle an arbitrary URL. Displays via in-game overlays where possible.
        /// This can be called from a non-thread-safe non-game-loaded state.
        /// </summary>
        /// <param name="url">The URL to load.</param>
        public void HandleLink(string url) => HandleLink(MessageFormatter.GetLinkDetails(url));

        /// <summary>
        /// Handle a specific <see cref="LinkDetails"/>.
        /// This can be called from a non-thread-safe non-game-loaded state.
        /// </summary>
        /// <param name="link">The link to load.</param>
        public void HandleLink(LinkDetails link) => Schedule(() =>
        {
            switch (link.Action)
            {
                case LinkAction.OpenChannel:
                    ShowChannel(link.Argument);
                    break;

                case LinkAction.OpenEditorTimestamp:
                case LinkAction.JoinMultiplayerMatch:
                case LinkAction.Spectate:
                    break;

                case LinkAction.External:
                    OpenUrlExternally(link.Argument);
                    break;

                case LinkAction.OpenUserProfile:
                    if (long.TryParse(link.Argument, out long userId))
                        ShowUser(userId);
                    break;

                default:
                    throw new NotImplementedException($"This {nameof(LinkAction)} ({link.Action.ToString()}) is missing an associated action.");
            }
        });

        public void OpenUrlExternally(string url) => waitForReady(() => externalLinkOpener, _ =>
        {
            if (url.StartsWith('/'))
                url = $"{API.Endpoint}{url}";

            externalLinkOpener.OpenUrlExternally(url);
        });

        public void ShowChannel(string channel) => waitForReady(() => channelManager, _ =>
        {
            try
            {
                channelManager.OpenChannel(channel);
            }
            catch (ChannelNotFoundException)
            {
                Logger.Log($"The requested channel \"{channel}\" does not exist");
            }
        });

        /// <summary>
        /// Show a user's profile as an overlay.
        /// </summary>
        /// <param name="userId">The user to display.</param>
        public void ShowUser(long userId) => waitForReady(() => userProfile, _ => userProfile.ShowUser(userId));

        protected virtual Loader CreateLoader() => new Loader();

        protected virtual UpdateManager CreateUpdateManager() => new UpdateManager();

        protected override Container CreateScalingContainer() => new ScalingContainer(ScalingMode.Everything);

        private ScheduledDelegate performFromMainMenuTask;

        /// <summary>
        /// Perform an action only after returning to a specific screen as indicated by <paramref name="validScreens"/>.
        /// Eagerly tries to exit the current screen until it succeeds.
        /// </summary>
        /// <param name="action">The action to perform once we are in the correct state.</param>
        /// <param name="validScreens">An optional collection of valid screen types. If any of these screens are already current we can perform the action immediately, else the first valid parent will be made current before performing the action. <see cref="MainMenu"/> is used if not specified.</param>
        public void PerformFromScreen(Action<IScreen> action, IEnumerable<Type> validScreens = null)
        {
            performFromMainMenuTask?.Cancel();

            validScreens ??= Enumerable.Empty<Type>();
            validScreens = validScreens.Append(typeof(MainMenu));

            CloseAllOverlays(false);

            // we may already be at the target screen type.
            if (validScreens.Contains(ScreenStack.CurrentScreen?.GetType()))
            {
                action(ScreenStack.CurrentScreen);
                return;
            }

            // find closest valid target
            IScreen screen = ScreenStack.CurrentScreen;

            while (screen != null)
            {
                if (validScreens.Contains(screen.GetType()))
                {
                    screen.MakeCurrent();
                    break;
                }

                screen = screen.GetParentScreen();
            }

            performFromMainMenuTask = Schedule(() => PerformFromScreen(action, validScreens));
        }

        /// <summary>
        /// Wait for the game (and target component) to become loaded and then run an action.
        /// </summary>
        /// <param name="retrieveInstance">A function to retrieve a (potentially not-yet-constructed) target instance.</param>
        /// <param name="action">The action to perform on the instance when load is confirmed.</param>
        /// <typeparam name="T">The type of the target instance.</typeparam>
        private void waitForReady<T>(Func<T> retrieveInstance, Action<T> action)
            where T : Drawable
        {
            var instance = retrieveInstance();

            if (ScreenStack == null || ScreenStack.CurrentScreen is StartupScreen || instance?.IsLoaded != true)
               Schedule(() => waitForReady(retrieveInstance, action));
            else
                action(instance);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            SentryLogger.Dispose();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // The next time this is updated is in UpdateAfterChildren, which occurs too late and results
            // in the cursor being shown for a few frames during the intro.
            // This prevents the cursor from showing until we have a screen with CursorVisible = true
            MenuCursorContainer.CanShowCursor = menuScreen?.CursorVisible ?? false;

            Container logoContainer;
            BackButton.Receptor receptor;

            dependencies.CacheAs(idleTracker = new GameIdleTracker(6000));

            AddRange(new Drawable[]
            {
                new VolumeControlReceptor
                {
                    RelativeSizeAxes = Axes.Both,
                    ActionRequested = action => volume.Adjust(action),
                    ScrollActionRequested = (action, amount, isPrecise) => volume.Adjust(action, amount, isPrecise),
                },
                screenContainer = new ScalingContainer(ScalingMode.ExcludeOverlays)
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        receptor = new BackButton.Receptor(),
                        ScreenStack = new PiouslyScreenStack { RelativeSizeAxes = Axes.Both },
                        BackButton = new BackButton(receptor)
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Action = () =>
                            {
                                var currentScreen = ScreenStack.CurrentScreen as IPiouslyScreen;

                                if (currentScreen?.AllowBackButton == true && !currentScreen.OnBackButton())
                                    ScreenStack.Exit();
                            }
                        },
                        logoContainer = new Container { RelativeSizeAxes = Axes.Both },
                    }
                },
                overlayContent = new Container { RelativeSizeAxes = Axes.Both },
                rightFloatingOverlayContent = new Container { RelativeSizeAxes = Axes.Both },
                leftFloatingOverlayContent = new Container { RelativeSizeAxes = Axes.Both },
                topMostOverlayContent = new Container { RelativeSizeAxes = Axes.Both },
                idleTracker,
                new ConfineMouseTracker()
            });

            ScreenStack.ScreenPushed += screenPushed;
            ScreenStack.ScreenExited += screenExited;

            loadComponentSingleFile(piouslyLogo, logo =>
            {
                logoContainer.Add(logo);

                // Loader has to be created after the logo has finished loading as Loader performs logo transformations on entering.
                ScreenStack.Push(CreateLoader().With(l => l.RelativeSizeAxes = Axes.Both));
            });

            loadComponentSingleFile(Toolbar = new Toolbar
            {
                OnHome = delegate
                {
                    CloseAllOverlays(false);
                    menuScreen?.MakeCurrent();
                },
            }, topMostOverlayContent.Add);

            loadComponentSingleFile(volume = new VolumeOverlay(), leftFloatingOverlayContent.Add, true);

            loadComponentSingleFile(new OnScreenDisplay(), Add, true);

            // dependency on notification overlay, dependent by settings overlay
            loadComponentSingleFile(CreateUpdateManager(), Add, true);

            // overlay elements
            //loadComponentSingleFile(new ManageCollectionsDialog(), overlayContent.Add, true);
            loadComponentSingleFile(dashboard = new DashboardOverlay(), overlayContent.Add, true);
            //loadComponentSingleFile(news = new NewsOverlay(), overlayContent.Add, true);
            //var rankingsOverlay = loadComponentSingleFile(new RankingsOverlay(), overlayContent.Add, true);
            loadComponentSingleFile(channelManager = new ChannelManager(), AddInternal, true);
            loadComponentSingleFile(chatOverlay = new ChatOverlay(), overlayContent.Add, true);
            loadComponentSingleFile(Settings = new SettingsOverlay { GetToolbarHeight = () => ToolbarOffset }, leftFloatingOverlayContent.Add, true);
            var changelogOverlay = loadComponentSingleFile(new ChangelogOverlay(), overlayContent.Add, true);
            loadComponentSingleFile(userProfile = new UserProfileOverlay(), overlayContent.Add, true);

            loadComponentSingleFile(new LoginOverlay
            {
                GetToolbarHeight = () => ToolbarOffset,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
            }, rightFloatingOverlayContent.Add, true);

            loadComponentSingleFile(new AccountCreationOverlay(), topMostOverlayContent.Add, true);
            loadComponentSingleFile(new DialogOverlay(), topMostOverlayContent.Add, true);
            loadComponentSingleFile(externalLinkOpener = new ExternalLinkOpener(), topMostOverlayContent.Add);

            chatOverlay.State.ValueChanged += state => channelManager.HighPollRate.Value = state.NewValue == Visibility.Visible;

            Add(externalLinkOpener = new ExternalLinkOpener());

            // side overlays which cancel each other.
            var singleDisplaySideOverlays = new OverlayContainer[] { Settings };

            foreach (var overlay in singleDisplaySideOverlays)
            {
                overlay.State.ValueChanged += state =>
                {
                    if (state.NewValue == Visibility.Hidden) return;

                    singleDisplaySideOverlays.Where(o => o != overlay).ForEach(o => o.Hide());
                };
            }

            // eventually informational overlays should be displayed in a stack, but for now let's only allow one to stay open at a time.
            var informationalOverlays = new OverlayContainer[] { userProfile };

            foreach (var overlay in informationalOverlays)
            {
                overlay.State.ValueChanged += state =>
                {
                    if (state.NewValue != Visibility.Hidden)
                        showOverlayAboveOthers(overlay, informationalOverlays);
                };
            }

            // ensure only one of these overlays are open at once.
            var singleDisplayOverlays = new OverlayContainer[] { chatOverlay, /*news,*/ dashboard, changelogOverlay, /*rankingsOverlay*/ };

            foreach (var overlay in singleDisplayOverlays)
            {
                overlay.State.ValueChanged += state =>
                {
                    // informational overlays should be dismissed on a show or hide of a full overlay.
                    informationalOverlays.ForEach(o => o.Hide());

                    if (state.NewValue != Visibility.Hidden)
                        showOverlayAboveOthers(overlay, singleDisplayOverlays);
                };
            }

            OverlayActivationMode.ValueChanged += mode =>
            {
                if (mode.NewValue != OverlayActivation.All) CloseAllOverlays();
            };

            void updateScreenOffset()
            {
                float offset = 0;

                if (Settings.State.Value == Visibility.Visible)
                    offset += Toolbar.HEIGHT / 2;

                screenContainer.MoveToX(offset, SettingsPanel.TRANSITION_LENGTH, Easing.OutQuint);
            }

            Settings.State.ValueChanged += _ => updateScreenOffset();
        }

        private void showOverlayAboveOthers(OverlayContainer overlay, OverlayContainer[] otherOverlays)
        {
            otherOverlays.Where(o => o != overlay).ForEach(o => o.Hide());

            // show above others if not visible at all, else leave at current depth.
            if (!overlay.IsPresent)
                overlayContent.ChangeChildDepth(overlay, (float)-Clock.CurrentTime);
        }

        private void forwardLoggedErrorsToNotifications()
        {
            int recentLogCount = 0;

            const double debounce = 60000;

            Logger.NewEntry += entry =>
            {
                if (entry.Level < LogLevel.Important || entry.Target == null) return;

                Interlocked.Increment(ref recentLogCount);
                Scheduler.AddDelayed(() => Interlocked.Decrement(ref recentLogCount), debounce);
            };
        }

        private Task asyncLoadStream;

        /// <summary>
        /// Queues loading the provided component in sequential fashion.
        /// This operation is limited to a single thread to avoid saturating all cores.
        /// </summary>
        /// <param name="component">The component to load.</param>
        /// <param name="loadCompleteAction">An action to invoke on load completion (generally to add the component to the hierarchy).</param>
        /// <param name="cache">Whether to cache the component as type <typeparamref name="T"/> into the game dependencies before any scheduling.</param>
        private T loadComponentSingleFile<T>(T component, Action<T> loadCompleteAction, bool cache = false)
            where T : Drawable
        {
            if (cache)
                dependencies.CacheAs(component);

            if (component is OverlayContainer overlay)
                overlays.Add(overlay);

            // schedule is here to ensure that all component loads are done after LoadComplete is run (and thus all dependencies are cached).
            // with some better organisation of LoadComplete to do construction and dependency caching in one step, followed by calls to loadComponentSingleFile,
            // we could avoid the need for scheduling altogether.
            Schedule(() =>
            {
                var previousLoadStream = asyncLoadStream;

                // chain with existing load stream
                asyncLoadStream = Task.Run(async () =>
                {
                    if (previousLoadStream != null)
                        await previousLoadStream;

                    try
                    {
                        Logger.Log($"Loading {component}...", level: LogLevel.Debug);

                        // Since this is running in a separate thread, it is possible for PiouslyGame to be disposed after LoadComponentAsync has been called
                        // throwing an exception. To avoid this, the call is scheduled on the update thread, which does not run if IsDisposed = true
                        Task task = null;
                        var del = new ScheduledDelegate(() => task = LoadComponentAsync(component, loadCompleteAction));
                        Scheduler.Add(del);

                        // The delegate won't complete if OsuGame has been disposed in the meantime
                        while (!IsDisposed && !del.Completed)
                            await Task.Delay(10);

                        // Either we're disposed or the load process has started successfully
                        if (IsDisposed)
                            return;

                        Debug.Assert(task != null);

                        await task;

                        Logger.Log($"Loaded {component}!", level: LogLevel.Debug);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                });
            });

            return component;
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            // forward any unhandled mouse scroll events to the volume control.
            volume.Adjust(GlobalAction.IncreaseVolume, e.ScrollDelta.Y, e.IsPrecise);
            return true;
        }

        public bool OnPressed(GlobalAction action)
        {
            if (introScreen == null) return false;

            switch (action)
            {
                case GlobalAction.ResetInputSettings:
                    var sensitivity = frameworkConfig.GetBindable<double>(FrameworkSetting.CursorSensitivity);

                    sensitivity.Disabled = false;
                    sensitivity.Value = 1;
                    sensitivity.Disabled = true;

                    frameworkConfig.Set(FrameworkSetting.IgnoredInputHandlers, string.Empty);
                    frameworkConfig.GetBindable<ConfineMouseMode>(FrameworkSetting.ConfineMouseMode).SetDefault();
                    return true;

                case GlobalAction.ToggleToolbar:
                    Toolbar.ToggleVisibility();
                    return true;
            }

            return false;
        }

        #region Inactive audio dimming

        private readonly BindableDouble inactiveVolumeFade = new BindableDouble();

        private void updateActiveState(bool isActive)
        {
            if (isActive)
                this.TransformBindableTo(inactiveVolumeFade, 1, 400, Easing.OutQuint);
            else
                this.TransformBindableTo(inactiveVolumeFade, LocalConfig.Get<double>(PiouslySetting.VolumeInactive), 4000, Easing.OutQuint);
        }

        #endregion

        public void OnReleased(GlobalAction action)
        {
        }

        private Container overlayContent;

        private Container rightFloatingOverlayContent;

        private Container leftFloatingOverlayContent;

        private Container topMostOverlayContent;

        [Resolved]
        private FrameworkConfigManager frameworkConfig { get; set; }

        private ScalingContainer screenContainer;

        protected override bool OnExiting()
        {
            if (ScreenStack.CurrentScreen is Loader)
                return false;

            if (introScreen?.DidLoadMenu == true && !(ScreenStack.CurrentScreen is IntroScreen))
            {
                Scheduler.Add(introScreen.MakeCurrent);
                return true;
            }

            return base.OnExiting();
        }

        /// <summary>
        /// Use to programatically exit the game as if the user was triggering via alt-f4.
        /// Will keep persisting until an exit occurs (exit may be blocked multiple times).
        /// </summary>
        public void GracefullyExit()
        {
            if (!OnExiting())
                Exit();
            else
                Scheduler.AddDelayed(GracefullyExit, 2000);
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            screenContainer.Padding = new MarginPadding { Top = ToolbarOffset };
            overlayContent.Padding = new MarginPadding { Top = ToolbarOffset };

            MenuCursorContainer.CanShowCursor = (ScreenStack.CurrentScreen as IPiouslyScreen)?.CursorVisible ?? false;
        }

        protected virtual void ScreenChanged(IScreen current, IScreen newScreen)
        {
            switch (newScreen)
            {
                case IntroScreen intro:
                    introScreen = intro;
                    break;

                case MainMenu menu:
                    menuScreen = menu;
                    break;
            }

            // reset on screen change for sanity.
            LocalUserPlaying.Value = false;

            if (current is IPiouslyScreen currentPiouslyScreen)
                OverlayActivationMode.UnbindFrom(currentPiouslyScreen.OverlayActivationMode);

            if (newScreen is IPiouslyScreen newPiouslyScreen)
            {
                OverlayActivationMode.BindTo(newPiouslyScreen.OverlayActivationMode);

                if (newPiouslyScreen.HideOverlaysOnEnter)
                    CloseAllOverlays();
                else
                    Toolbar.Show();

                if (newPiouslyScreen.AllowBackButton)
                    BackButton.Show();
                else
                    BackButton.Hide();
            }
        }

        private void screenPushed(IScreen lastScreen, IScreen newScreen)
        {
            ScreenChanged(lastScreen, newScreen);
            Logger.Log($"Screen changed → {newScreen}");
        }

        private void screenExited(IScreen lastScreen, IScreen newScreen)
        {
            ScreenChanged(lastScreen, newScreen);
            Logger.Log($"Screen changed ← {newScreen}");

            if (newScreen == null)
                Exit();
        }
    }
}