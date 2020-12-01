using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osu.Framework.Threading;
using Piously.Game.Configuration;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Containers;
using Piously.Game.Screens.Menu;
using Piously.Game.Screens.Backgrounds;
using Piously.Game.Input;
using Piously.Game.Input.Bindings;
using Piously.Game.Overlays;
using Piously.Game.Screens;
using osuTK.Graphics;
using LogLevel = osu.Framework.Logging.LogLevel;

namespace Piously.Game
{
    //The actual game, specifically loads the UI
    public class PiouslyGame : PiouslyGameBase, IKeyBindingHandler<GlobalAction>
    {
        private ScreenStack mainMenuStack;
        private MainMenu mainMenu;
        private BackgroundScreenStack backgroundStack;
        private BackgroundScreen background;

        private Container overlayContent;
        private Container leftFloatingOverlayContent;
        private Container rightFloatingOverlayContent;
        private Container topMostOverlayContent;

        SettingsOverlay settings;

        private ScalingContainer screenContainer;

        private readonly List<OverlayContainer> overlays = new List<OverlayContainer>();
        private readonly List<OverlayContainer> visibleBlockingOverlays = new List<OverlayContainer>();

        private void updateBlockingOverlayFade() =>
            screenContainer.FadeColour(visibleBlockingOverlays.Any() ? PiouslyColour.Gray(0.5f) : Color4.White, 500, Easing.OutQuint);

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
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance)
            {
                Logger.Log(@"Piously does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            dependencies.CacheAs(this);

            dependencies.Cache(new PiouslyColour());
        }

        protected override Container CreateScalingContainer() => new ScalingContainer(ScalingMode.Everything);

        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

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

                        // The delegate won't complete if PiouslyGame has been disposed in the meantime
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

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // The next time this is updated is in UpdateAfterChildren, which occurs too late and results
            // in the cursor being shown for a few frames during the intro.
            // This prevents the cursor from showing until we have a screen with CursorVisible = true
            MenuCursorContainer.CanShowCursor = true; //TEMP

            backgroundStack = new BackgroundScreenStack();
            mainMenuStack = new ScreenStack();

            background = new BackgroundScreen();
            mainMenu = new MainMenu();

            backgroundStack.Push(background);
            mainMenuStack.Push(mainMenu);

            AddRange(new Drawable[]
            {
                screenContainer = new ScalingContainer(ScalingMode.ExcludeOverlays)
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        backgroundStack,
                        mainMenuStack,
                    }
                },
                overlayContent = new Container { RelativeSizeAxes = Axes.Both },
                rightFloatingOverlayContent = new Container { RelativeSizeAxes = Axes.Both },
                leftFloatingOverlayContent = new Container { RelativeSizeAxes = Axes.Both },
                topMostOverlayContent = new Container { RelativeSizeAxes = Axes.Both },
                new ConfineMouseTracker(),
            });

            loadComponentSingleFile(settings = new SettingsOverlay(), leftFloatingOverlayContent.Add, true);
        }

        protected override void Update()
        {
            base.Update();
        }

        public bool OnPressed(GlobalAction action)
        {
            if(action == GlobalAction.ToggleSettings)
            {
                settings.ToggleVisibility();
                return true;
            }
            return false;
        }

        public void OnReleased(GlobalAction action)
        {
        }
    }
}