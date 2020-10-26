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
using Piously.Game.Updater;
using LogLevel = osu.Framework.Logging.LogLevel;

namespace Piously.Game
{
    public class PiouslyGame : PiouslyGameBase, IKeyBindingHandler<GlobalAction>
    {
        public Toolbar Toolbar;

        private ChatOverlay chatOverlay;

        private ChannelManager channelManager;

        private UserProfileOverlay userProfile;

        /// <summary>
        /// Whether overlays should be able to be opened game-wide. Value is sourced from the current active screen.
        /// </summary>
        public readonly IBindable<OverlayActivation> OverlayActivationMode = new Bindable<OverlayActivation>();

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

        public void ShowUser(long userId) => waitForReady(() => userProfile, _ => userProfile.ShowUser(userId));

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

        private ScalingContainer screenContainer;
    }
}