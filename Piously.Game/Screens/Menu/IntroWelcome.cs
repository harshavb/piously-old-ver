using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using Piously.Game.Screens.Backgrounds;
using osuTK.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class IntroWelcome : IntroScreen
    {
        private const double delay_step_two = 2142;
        private SampleChannel welcome;
        private SampleChannel pianoReverb;
        protected override string SeeyaSampleName => "Intro/Welcome/seeya";

        protected override BackgroundScreen CreateBackground() => background = new BackgroundScreenDefault(false)
        {
            Alpha = 0,
        };

        private BackgroundScreenDefault background;

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            if (MenuVoice.Value)
                welcome = audio.Samples.Get(@"Intro/Welcome/welcome");

            pianoReverb = audio.Samples.Get(@"Intro/Welcome/welcome_piano");
        }

        protected override void LogoArriving(PiouslyLogo logo, bool resuming)
        {
            base.LogoArriving(logo, resuming);

            if (!resuming)
            {
                Track.Looping = true;

                LoadComponentAsync(new WelcomeIntroSequence
                {
                    RelativeSizeAxes = Axes.Both
                }, intro =>
                {
                    PrepareMenuLoad();

                    intro.LogoVisualization.AddAmplitudeSource(pianoReverb);

                    AddInternal(intro);

                    welcome?.Play();
                    pianoReverb?.Play();

                    Scheduler.AddDelayed(() =>
                    {
                        StartTrack();

                        const float fade_in_time = 200;

                        logo.ScaleTo(1);
                        logo.FadeIn(fade_in_time);

                        background.FadeIn(fade_in_time);

                        LoadMenu();
                    }, delay_step_two);
                });
            }
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);
            background.FadeOut(100);
        }

        private class WelcomeIntroSequence : Container
        {
            private Sprite welcomeText;
            private Container scaleContainer;

            public LogoVisualization LogoVisualization { get; private set; }

            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                Origin = Anchor.Centre;
                Anchor = Anchor.Centre;

                Children = new Drawable[]
                {
                    scaleContainer = new Container
                    {
                        AutoSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Children = new Drawable[]
                        {
                            LogoVisualization = new LogoVisualization
                            {
                                RelativeSizeAxes = Axes.Both,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Alpha = 0.5f,
                                AccentColor = Color4.DarkBlue,
                                Size = new Vector2(0.96f)
                            },
                            new Circle
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Size = new Vector2(480),
                                Colour = Color4.Black
                            },
                            welcomeText = new Sprite
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Texture = textures.Get(@"Intro/Welcome/welcome_text")
                            },
                        }
                    },
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                using (BeginDelayedSequence(0, true))
                {
                    scaleContainer.ScaleTo(0.9f).ScaleTo(1, delay_step_two).OnComplete(_ => Expire());
                    scaleContainer.FadeInFromZero(1800);

                    welcomeText.ScaleTo(new Vector2(1, 0)).ScaleTo(Vector2.One, 400, Easing.Out);
                }
            }
        }
    }
}
