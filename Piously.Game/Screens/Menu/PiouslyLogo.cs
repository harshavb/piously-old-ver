using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using Piously.Game.Graphics.Backgrounds;
using Piously.Game.Graphics.Containers;
using osuTK;
using osuTK.Graphics;
using Piously.Game.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class PiouslyLogo : PiouslyClickableContainer
    {
        public readonly Color4 PiouslyYellow = new PiouslyColor().Yellow;

        private const double transition_length = 300;

        private readonly Sprite logo;

        private readonly IntroSequence intro;

        private readonly CircularContainer logoContainer;
        private readonly Container logoHoverContainer;
        private readonly Container colourAndHexagons;
        private readonly Hexagons hexagons;

        /// <summary>
        /// Return value decides whether the logo should play its own sample for the click action.
        /// </summary>
        public Func<bool> Action;

        /// <summary>
        /// The size of the logo Sprite with respect to the scale of its hover and bounce containers.
        /// </summary>
        /// <remarks>Does not account for the scale of this <see cref="OsuLogo"/></remarks>
        public float SizeForFlow => logo == null ? 0 : logo.DrawSize.X * logo.Scale.X * logoHoverContainer.Scale.X;

        public bool IsTracking { get; set; }

        private readonly Sprite ripple;

        private readonly Container rippleContainer;

        public bool Hexagons
        {
            set => colourAndHexagons.FadeTo(value ? 1 : 0, transition_length, Easing.OutQuint);
        }

        public bool BeatMatching = true;
/*
        public bool Ripple
        {
            get => rippleContainer.Alpha > 0;
            set => rippleContainer.FadeTo(value ? 1 : 0, transition_length, Easing.OutQuint);
        }
*/
        private readonly Box flashLayer;

        private readonly Container impactContainer;

        private const double early_activation = 60;

        public override bool IsPresent => base.IsPresent || Scheduler.HasPendingTasks;

        public PiouslyLogo()
        {
            Origin = Anchor.Centre;

            AutoSizeAxes = Axes.Both;

            Children = new Drawable[]
            {
                intro = new IntroSequence
                {
                    RelativeSizeAxes = Axes.Both,
                },
                logoHoverContainer = new Container
                {
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        logoContainer = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Scale = new Vector2(0.88f),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                colourAndHexagons = new Container
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Colour = PiouslyYellow,
                                        },
                                        hexagons = new Hexagons
                                        {
                                            HexagonScale = 4,
                                            ColorLight = Color4Extensions.FromHex(@"ff7db7"),
                                            ColorDark = Color4Extensions.FromHex(@"de5b95"),
                                            RelativeSizeAxes = Axes.Both,
                                        },
                                    }
                                },
                                flashLayer = new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Blending = BlendingParameters.Additive,
                                    Colour = Color4.White,
                                    Alpha = 0,
                                },
                            },
                        }
                    }
                },
                logo = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },                       
            };
        }

        /// <summary>
        /// Schedule a new external animation. Handled queueing and finishing previous animations in a sane way.
        /// </summary>
        /// <param name="action">The animation to be performed</param>
        /// <param name="waitForPrevious">If true, the new animation is delayed until all previous transforms finish. If false, existing transformed are cleared.</param>
        public void AppendAnimatingAction(Action action, bool waitForPrevious)
        {
            void runnableAction()
            {
                if (waitForPrevious)
                    this.DelayUntilTransformsFinished().Schedule(action);
                else
                {
                    ClearTransforms();
                    action();
                }
            }

            if (IsLoaded)
                runnableAction();
            else
                Schedule(runnableAction);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            logo.Texture = textures.Get(@"Menu/logo");
            //ripple.Texture = textures.Get(@"Menu/logo");
        }

        public void PlayIntro()
        {
            const double length = 3150;
            const double fade = 200;

            intro.Show();
            intro.Start(length);
            intro.Delay(length + fade).FadeOut();
        }

        protected override void Update()
        {
            base.Update();

            const float paused_velocity = 0.5f;
                
            hexagons.Velocity = paused_velocity;
        }

        public override bool HandlePositionalInput => base.HandlePositionalInput && Action != null && Alpha > 0.2f;

        protected override bool OnHover(HoverEvent e)
        {
            logoHoverContainer.ScaleTo(1.1f, 500, Easing.OutElastic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            logoHoverContainer.ScaleTo(1, 500, Easing.OutElastic);
        }

        public void Impact()
        {
            impactContainer.FadeOutFromOne(250, Easing.In);
            impactContainer.ScaleTo(0.96f);
            impactContainer.ScaleTo(1.12f, 250);
        }
    }
}
