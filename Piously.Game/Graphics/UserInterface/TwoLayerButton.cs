using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using Piously.Game.Graphics.Sprites;
using osu.Framework.Extensions.Color4Extensions;
using Piously.Game.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using Piously.Game.Screens.Select;

namespace Piously.Game.Graphics.UserInterface
{
    public class TwoLayerButton : PiouslyClickableContainer
    {
        private readonly BouncingIcon bouncingIcon;

        public Box IconLayer;
        public Box TextLayer;

        private const int transform_time = 600;
        private const int pulse_length = 250;

        private const float shear_width = 5f;

        private static readonly Vector2 shear = new Vector2(shear_width / Footer.HEIGHT, 0);

        public static readonly Vector2 SIZE_EXTENDED = new Vector2(140, 50);
        public static readonly Vector2 SIZE_RETRACTED = new Vector2(100, 50);
        private readonly SpriteText text;

        public Color4 HoverColour;
        private readonly Container c1;
        private readonly Container c2;

        public Color4 BackgroundColour
        {
            set
            {
                TextLayer.Colour = value;
                IconLayer.Colour = value;
            }
        }

        public override Anchor Origin
        {
            get => base.Origin;
            set
            {
                base.Origin = value;
                c1.Origin = c1.Anchor = value.HasFlag(Anchor.x2) ? Anchor.TopLeft : Anchor.TopRight;
                c2.Origin = c2.Anchor = value.HasFlag(Anchor.x2) ? Anchor.TopRight : Anchor.TopLeft;

                X = value.HasFlag(Anchor.x2) ? SIZE_RETRACTED.X * shear.X * 0.5f : 0;

                Remove(c1);
                Remove(c2);
                c1.Depth = value.HasFlag(Anchor.x2) ? 0 : 1;
                c2.Depth = value.HasFlag(Anchor.x2) ? 1 : 0;
                Add(c1);
                Add(c2);
            }
        }

        public TwoLayerButton()
        {
            Size = SIZE_RETRACTED;
            Shear = shear;

            Children = new Drawable[]
            {
                c2 = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.4f,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Masking = true,
                            MaskingSmoothness = 2,
                            EdgeEffect = new EdgeEffectParameters
                            {
                                Type = EdgeEffectType.Shadow,
                                Colour = Color4.Black.Opacity(0.2f),
                                Offset = new Vector2(2, 0),
                                Radius = 2,
                            },
                            Children = new[]
                            {
                                IconLayer = new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    EdgeSmoothness = new Vector2(2, 0),
                                },
                            }
                        },
                        bouncingIcon = new BouncingIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Shear = -shear,
                        },
                    }
                },
                c1 = new Container
                {
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.6f,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Masking = true,
                            MaskingSmoothness = 2,
                            EdgeEffect = new EdgeEffectParameters
                            {
                                Type = EdgeEffectType.Shadow,
                                Colour = Color4.Black.Opacity(0.2f),
                                Offset = new Vector2(2, 0),
                                Radius = 2,
                            },
                            Children = new[]
                            {
                                TextLayer = new Box
                                {
                                    Origin = Anchor.TopLeft,
                                    Anchor = Anchor.TopLeft,
                                    RelativeSizeAxes = Axes.Both,
                                    EdgeSmoothness = new Vector2(2, 0),
                                },
                            }
                        },
                        text = new PiouslySpriteText
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Shear = -shear,
                        }
                    }
                },
            };
        }

        public IconUsage Icon
        {
            set => bouncingIcon.Icon = value;
        }

        public string Text
        {
            set => text.Text = value;
        }

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => IconLayer.ReceivePositionalInputAt(screenSpacePos) || TextLayer.ReceivePositionalInputAt(screenSpacePos);

        protected override bool OnHover(HoverEvent e)
        {
            this.ResizeTo(SIZE_EXTENDED, transform_time, Easing.OutElastic);

            IconLayer.FadeColour(HoverColour, transform_time / 2f, Easing.OutQuint);

            bouncingIcon.ScaleTo(1.1f, transform_time, Easing.OutElastic);

            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ResizeTo(SIZE_RETRACTED, transform_time, Easing.Out);
            IconLayer.FadeColour(TextLayer.Colour, transform_time, Easing.Out);

            bouncingIcon.ScaleTo(1, transform_time, Easing.Out);
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;

        protected override bool OnClick(ClickEvent e)
        {
            var flash = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.White.Opacity(0.5f),
            };
            Add(flash);

            flash.Alpha = 1;
            flash.FadeOut(500, Easing.OutQuint);
            flash.Expire();

            return base.OnClick(e);
        }

        private class BouncingIcon : ScalingContainer
        {
            private readonly SpriteIcon icon;

            public IconUsage Icon
            {
                set => icon.Icon = value;
            }

            public BouncingIcon()
            {
                Children = new Drawable[]
                {
                    icon = new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(25),
                    }
                };
            }
        }
    }
}
