using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Overlays
{
    public abstract class OverlayTabControl<T> : PiouslyTabControl<T>
    {
        private readonly Box bar;

        protected float BarHeight
        {
            set => bar.Height = value;
        }

        public override Color4 AccentColor
        {
            get => base.AccentColor;
            set
            {
                base.AccentColor = value;
                bar.Colour = value;
            }
        }

        protected OverlayTabControl()
        {
            TabContainer.Masking = false;
            TabContainer.Spacing = new Vector2(20, 0);

            AddInternal(bar = new Box
            {
                RelativeSizeAxes = Axes.X,
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft
            });
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider)
        {
            AccentColor = colorProvider.Highlight1;
        }

        protected override Dropdown<T> CreateDropdown() => null;

        protected override TabItem<T> CreateTabItem(T value) => new OverlayTabItem(value);

        protected class OverlayTabItem : TabItem<T>, IHasAccentColor
        {
            protected readonly ExpandingBar Bar;
            protected readonly PiouslySpriteText Text;

            private Color4 accentColor;

            public Color4 AccentColor
            {
                get => accentColor;
                set
                {
                    if (accentColor == value)
                        return;

                    accentColor = value;
                    Bar.Colour = value;

                    updateState();
                }
            }

            public OverlayTabItem(T value)
                : base(value)
            {
                AutoSizeAxes = Axes.X;
                RelativeSizeAxes = Axes.Y;

                Children = new Drawable[]
                {
                    Text = new PiouslySpriteText
                    {
                        Margin = new MarginPadding { Bottom = 10 },
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                        Font = PiouslyFont.GetFont(),
                    },
                    Bar = new ExpandingBar
                    {
                        Anchor = Anchor.BottomCentre,
                        ExpandedSize = 5f,
                        CollapsedSize = 0
                    },
                    new HoverClickSounds()
                };
            }

            protected override bool OnHover(HoverEvent e)
            {
                base.OnHover(e);

                if (!Active.Value)
                    HoverAction();

                return true;
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                base.OnHoverLost(e);

                if (!Active.Value)
                    UnhoverAction();
            }

            protected override void OnActivated()
            {
                HoverAction();
                Text.Font = Text.Font.With(weight: FontWeight.Bold);
                Text.FadeColour(Color4.White, 120, Easing.InQuad);
            }

            protected override void OnDeactivated()
            {
                UnhoverAction();
                Text.Font = Text.Font.With(weight: FontWeight.Medium);
            }

            private void updateState()
            {
                if (Active.Value)
                    OnActivated();
                else
                    OnDeactivated();
            }

            protected virtual void HoverAction() => Bar.Expand();

            protected virtual void UnhoverAction()
            {
                Bar.Collapse();
                Text.FadeColour(AccentColor, 120, Easing.InQuad);
            }
        }
    }
}
