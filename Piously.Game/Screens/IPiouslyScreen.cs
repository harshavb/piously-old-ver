using osu.Framework.Bindables;
using osu.Framework.Screens;
using Piously.Game.Overlays;

namespace Piously.Game.Screens
{
    public interface IPiouslyScreen : IScreen
    {
        /// <summary>
        /// Whether the user can exit this this <see cref="IOsuScreen"/> by pressing the back button.
        /// </summary>
        bool AllowBackButton { get; }

        /// <summary>
        /// Whether a top-level component should be allowed to exit the current screen to, for example,
        /// complete an import. Note that this can be overridden by a user if they specifically request.
        /// </summary>
        bool AllowExternalScreenChange { get; }

        /// <summary>
        /// Whether this <see cref="OsuScreen"/> allows the cursor to be displayed.
        /// </summary>
        bool CursorVisible { get; }

        /// <summary>
        /// Whether all overlays should be hidden when this screen is entered or resumed.
        /// </summary>
        bool HideOverlaysOnEnter { get; }

        /// <summary>
        /// Whether overlays should be able to be opened when this screen is current.
        /// </summary>
        IBindable<OverlayActivation> OverlayActivationMode { get; }

        /// <summary>
        /// The amount of parallax to be applied while this screen is displayed.
        /// </summary>
        float BackgroundParallaxAmount { get; }

        /// <summary>
        /// Invoked when the back button has been pressed to close any overlays before exiting this <see cref="IOsuScreen"/>.
        /// </summary>
        /// <remarks>
        /// Return <c>true</c> to block this <see cref="IOsuScreen"/> from being exited after closing an overlay.
        /// Return <c>false</c> if this <see cref="IOsuScreen"/> should continue exiting.
        /// </remarks>
        bool OnBackButton();
    }
}
