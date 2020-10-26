using osuTK;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyTabDropdown<T> : PiouslyDropdown<T>
    {
        public PiouslyTabDropdown()
        {
            RelativeSizeAxes = Axes.X;
        }

        protected override DropdownMenu CreateMenu() => new PiouslyTabDropdownMenu();

        protected override DropdownHeader CreateHeader() => new PiouslyTabDropdownHeader
        {
            Anchor = Anchor.TopRight,
            Origin = Anchor.TopRight
        };

        private class PiouslyTabDropdownMenu : PiouslyDropdownMenu
        {
            public PiouslyTabDropdownMenu()
            {
                Anchor = Anchor.TopRight;
                Origin = Anchor.TopRight;

                BackgroundColour = Color4.Black.Opacity(0.7f);
                MaxHeight = 400;
            }

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawablePiouslyTabDropdownMenuItem(item) { AccentColor = AccentColor };

            private class DrawablePiouslyTabDropdownMenuItem : DrawablePiouslyDropdownMenuItem
            {
                public DrawablePiouslyTabDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    ForegroundColourHover = Color4.Black;
                }
            }
        }

        protected class PiouslyTabDropdownHeader : PiouslyDropdownHeader
        {
            public override Color4 AccentColor
            {
                get => base.AccentColor;
                set
                {
                    base.AccentColor = value;
                    Foreground.Colour = value;
                }
            }

            public PiouslyTabDropdownHeader()
            {
                RelativeSizeAxes = Axes.None;
                AutoSizeAxes = Axes.X;

                BackgroundColour = Color4.Black.Opacity(0.5f);

                Background.Height = 0.5f;
                Background.CornerRadius = 5;
                Background.Masking = true;

                Foreground.RelativeSizeAxes = Axes.None;
                Foreground.AutoSizeAxes = Axes.X;
                Foreground.RelativeSizeAxes = Axes.Y;
                Foreground.Margin = new MarginPadding(5);

                Foreground.Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Icon = FontAwesome.Solid.EllipsisH,
                        Size = new Vector2(14),
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                    }
                };

                Padding = new MarginPadding { Left = 5, Right = 5 };
            }

            protected override bool OnHover(HoverEvent e)
            {
                Foreground.Colour = BackgroundColour;
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                Foreground.Colour = BackgroundColourHover;
                base.OnHoverLost(e);
            }
        }
    }
}
