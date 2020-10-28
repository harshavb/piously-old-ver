using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Piously.Game.Screens.Menu;
using Piously.Game.Overlays;
using Piously.Game.Users;
using Piously.Game.Online.API;

namespace Piously.Game.Screens
{
    public abstract class PiouslyScreen : Screen, IPiouslyScreen, IHasDescription
    {
        /// <summary>
        /// The amount of negative padding that should be applied to game background content which touches both the left and right sides of the screen.
        /// This allows for the game content to be pushed by the options/notification overlays without causing black areas to appear.
        /// </summary>
        public const float HORIZONTAL_OVERFLOW_PADDING = 50;

        /// <summary>
        /// A user-facing title for this screen.
        /// </summary>
        public virtual string Title => GetType().Name;

        public string Description => Title;

        public virtual bool AllowBackButton => true;

        public virtual bool AllowExternalScreenChange => false;

        /// <summary>
        /// Whether all overlays should be hidden when this screen is entered or resumed.
        /// </summary>
        public virtual bool HideOverlaysOnEnter => false;

        /// <summary>
        /// The initial overlay activation mode to use when this screen is entered for the first time.
        /// </summary>
        protected virtual OverlayActivation InitialOverlayActivationMode => OverlayActivation.All;

        protected readonly Bindable<OverlayActivation> OverlayActivationMode;

        IBindable<OverlayActivation> IPiouslyScreen.OverlayActivationMode => OverlayActivationMode;

        public virtual bool CursorVisible => true;
        
        protected new PiouslyGameBase Game => base.Game as PiouslyGameBase;

        /// <summary>
        /// The <see cref="UserActivity"/> to set the user's activity automatically to when this screen is entered
        /// <para>This <see cref="Activity"/> will be automatically set to <see cref="InitialActivity"/> for this screen on entering unless
        /// <see cref="Activity"/> is manually set before.</para>
        /// </summary>
        protected virtual UserActivity InitialActivity => null;

        private UserActivity activity;

        /// <summary>
        /// The current <see cref="UserActivity"/> for this screen.
        /// </summary>
        protected UserActivity Activity
        {
            get => activity;
            set
            {
                if (value == activity) return;

                activity = value;
                updateActivity();
            }
        }

        private SampleChannel sampleExit;

        protected virtual bool PlayResumeSound => true;

        public virtual float BackgroundParallaxAmount => 1;

        public virtual bool AllowRateAdjustments => true;

        protected BackgroundScreen Background => backgroundStack?.CurrentScreen as BackgroundScreen;

        private BackgroundScreen localBackground;

        [Resolved(canBeNull: true)]
        private BackgroundScreenStack backgroundStack { get; set; }

        [Resolved(canBeNull: true)]
        private PiouslyLogo logo { get; set; }

        [Resolved(canBeNull: true)]
        private IAPIProvider api { get; set; }

        protected PiouslyScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            OverlayActivationMode = new Bindable<OverlayActivation>(InitialOverlayActivationMode);
        }

        [BackgroundDependencyLoader(true)]
        private void load(PiouslyGame osu, AudioManager audio)
        {
            sampleExit = audio.Samples.Get(@"UI/screen-back");
        }

        public override void OnResuming(IScreen last)
        {
            if (PlayResumeSound)
                sampleExit?.Play();
            applyArrivingDefaults(true);

            updateActivity();

            base.OnResuming(last);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);

            onSuspendingLogo();
        }

        public override void OnEntering(IScreen last)
        {
            applyArrivingDefaults(false);

            backgroundStack?.Push(localBackground = CreateBackground());

            if (activity == null)
                Activity = InitialActivity;

            base.OnEntering(last);
        }

        public override bool OnExiting(IScreen next)
        {
            if (ValidForResume && logo != null)
                onExitingLogo();

            if (base.OnExiting(next))
                return true;

            if (localBackground != null && backgroundStack?.CurrentScreen == localBackground)
                backgroundStack?.Exit();

            return false;
        }

        private void updateActivity()
        {
            if (api != null)
                api.Activity.Value = activity;
        }

        /// <summary>
        /// Fired when this screen was entered or resumed and the logo state is required to be adjusted.
        /// </summary>
        protected virtual void LogoArriving(PiouslyLogo logo, bool resuming)
        {
            ApplyLogoArrivingDefaults(logo);
        }

        private void applyArrivingDefaults(bool isResuming)
        {
            logo?.AppendAnimatingAction(() =>
            {
                if (this.IsCurrentScreen()) LogoArriving(logo, isResuming);
            }, true);
        }

        /// <summary>
        /// Applies default animations to an arriving logo.
        /// Todo: This should not exist.
        /// </summary>
        /// <param name="logo">The logo to apply animations to.</param>
        public static void ApplyLogoArrivingDefaults(PiouslyLogo logo)
        {
            logo.Action = null;
            logo.FadeOut(300, Easing.OutQuint);
            logo.Anchor = Anchor.TopLeft;
            logo.Origin = Anchor.Centre;
            logo.RelativePositionAxes = Axes.Both;
            logo.BeatMatching = true;
            logo.Hexagons = true;
            logo.Ripple = true;
        }

        private void onExitingLogo()
        {
            logo?.AppendAnimatingAction(() => LogoExiting(logo), false);
        }

        /// <summary>
        /// Fired when this screen was exited to add any outwards transition to the logo.
        /// </summary>
        protected virtual void LogoExiting(PiouslyLogo logo)
        {
        }

        private void onSuspendingLogo()
        {
            logo?.AppendAnimatingAction(() => LogoSuspending(logo), false);
        }

        /// <summary>
        /// Fired when this screen was suspended to add any outwards transition to the logo.
        /// </summary>
        protected virtual void LogoSuspending(PiouslyLogo logo)
        {
        }

        /// <summary>
        /// Override to create a BackgroundMode for the current screen.
        /// Note that the instance created may not be the used instance if it matches the BackgroundMode equality clause.
        /// </summary>
        protected virtual BackgroundScreen CreateBackground() => null;

        public virtual bool OnBackButton() => false;
    }
}
