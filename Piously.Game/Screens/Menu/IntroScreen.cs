using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Piously.Game.Configuration;
using Piously.Game.Screens.Backgrounds;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Screens.Menu
{
    public abstract class IntroScreen : StartupScreen
    {
        /// <summary>
        /// Whether we have loaded the menu previously.
        /// </summary>
        public bool DidLoadMenu { get; private set; }

        protected IBindable<bool> MenuVoice { get; private set; }

        protected IBindable<bool> MenuMusic { get; private set; }

        protected ITrack Track { get; private set; }

        private const int exit_delay = 3000;

        private SampleChannel seeya;

        protected virtual string SeeyaSampleName => "Intro/seeya";

        private MainMenu mainMenu;

        [Resolved]
        private AudioManager audio { get; set; }

        /// <summary>
        /// Whether the <see cref="Track"/> is provided by osu! resources, rather than a user beatmap.
        /// Only valid during or after <see cref="LogoArriving"/>.
        /// </summary>
        protected bool UsingThemedIntro { get; private set; }

        [BackgroundDependencyLoader]
        private void load(PiouslyConfigManager config, osu.Framework.Game game)
        {
            // prevent user from changing beatmap while the intro is still runnning.

            MenuVoice = config.GetBindable<bool>(PiouslySetting.MenuVoice);
            MenuMusic = config.GetBindable<bool>(PiouslySetting.MenuMusic);
            seeya = audio.Samples.Get(SeeyaSampleName);

            loadThemedIntro();

            bool loadThemedIntro()
            {
                return UsingThemedIntro;
            }
        }

        public override void OnResuming(IScreen last)
        {
            this.FadeIn(300);

            double fadeOutTime = exit_delay;

            // we also handle the exit transition.
            if (MenuVoice.Value)
            {
                seeya.Play();
            }
            else
            {
                fadeOutTime = 500;
            }

            //don't want to fade out completely else we will stop running updates.
            Game.FadeTo(0.01f, fadeOutTime).OnComplete(_ => this.Exit());

            base.OnResuming(last);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);
        }

        protected override BackgroundScreen CreateBackground() => new BackgroundScreenBlack();

        protected void StartTrack()
        {
            // Only start the current track if it is the menu music. A beatmap's track is started when entering the Main Menu.
            if (UsingThemedIntro)
                Track.Restart();
        }

        protected override void LogoArriving(PiouslyLogo logo, bool resuming)
        {
            base.LogoArriving(logo, resuming);

            logo.Colour = Color4.White;
            logo.Hexagons = false;
            logo.Ripple = false;

            if (!resuming)
            {

                logo.MoveTo(new Vector2(0.5f));
                logo.ScaleTo(Vector2.One);
                logo.Hide();
            }
            else
            {
                const int quick_appear = 350;
                var initialMovementTime = logo.Alpha > 0.2f ? quick_appear : 0;

                logo.MoveTo(new Vector2(0.5f), initialMovementTime, Easing.OutQuint);

                logo
                    .ScaleTo(1, initialMovementTime, Easing.OutQuint)
                    .FadeIn(quick_appear, Easing.OutQuint)
                    .Then()
                    .RotateTo(20, exit_delay * 1.5f)
                    .FadeOut(exit_delay);
            }
        }

        protected void PrepareMenuLoad() => LoadComponentAsync(mainMenu = new MainMenu());

        protected void LoadMenu()
        {

            DidLoadMenu = true;
            this.Push(mainMenu);
        }
    }
}
