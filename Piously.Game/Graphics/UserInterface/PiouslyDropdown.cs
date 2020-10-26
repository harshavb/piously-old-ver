using System.Linq;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Sprites;
using osuTK;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyDropdown<T> : Dropdown<T>, IHasAccentColor
    {
        private const float corner_radius = 4;

        private Color4 accentColor;

        public Color4 AccentColor
        {
            get => accentColor;
            set
            {
                accentColor = value;
                updateAccentColor();
            }
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            if (accentColor == default)
                accentColor = colors.PinkDarker;
            updateAccentColor();
        }

        private void updateAccentColor()
        {
            if (Header is IHasAccentColor header) header.AccentColor = accentColor;

            if (Menu is IHasAccentColor menu) menu.AccentColor = accentColor;
        }

        protected override DropdownHeader CreateHeader() => new PiouslyDropdownHeader();

        protected override DropdownMenu CreateMenu() => new PiouslyDropdownMenu();

        #region PiouslyDropdownMenu

        protected class PiouslyDropdownMenu : DropdownMenu, IHasAccentColor
        {
            public override bool HandleNonPositionalInput => State == MenuState.Open;

            // todo: this uses the same styling as OsuMenu. hopefully we can just use OsuMenu in the future with some refactoring
            public PiouslyDropdownMenu()
            {
                CornerRadius = corner_radius;
                BackgroundColour = Color4.Black.Opacity(0.5f);

                MaskingContainer.CornerRadius = corner_radius;

                // todo: this uses the same styling as OsuMenu. hopefully we can just use OsuMenu in the future with some refactoring
                ItemsContainer.Padding = new MarginPadding(5);
            }

            // todo: this uses the same styling as OsuMenu. hopefully we can just use OsuMenu in the future with some refactoring
            protected override void AnimateOpen() => this.FadeIn(300, Easing.OutQuint);
            protected override void AnimateClose() => this.FadeOut(300, Easing.OutQuint);

            // todo: this uses the same styling as OsuMenu. hopefully we can just use OsuMenu in the future with some refactoring
            protected override void UpdateSize(Vector2 newSize)
            {
                if (Direction == Direction.Vertical)
                {
                    Width = newSize.X;
                    this.ResizeHeightTo(newSize.Y, 300, Easing.OutQuint);
                }
                else
                {
                    Height = newSize.Y;
                    this.ResizeWidthTo(newSize.X, 300, Easing.OutQuint);
                }
            }

            private Color4 accentColor;

            public Color4 AccentColor
            {
                get => accentColor;
                set
                {
                    accentColor = value;
                    foreach (var c in Children.OfType<IHasAccentColor>())
                        c.AccentColor = value;
                }
            }

            protected override Menu CreateSubMenu() => new PiouslyMenu(Direction.Vertical);

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawablePiouslyDropdownMenuItem(item) { AccentColor = accentColor };

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new PiouslyScrollContainer(direction);

            #region DrawableOsuDropdownMenuItem

            public class DrawablePiouslyDropdownMenuItem : DrawableDropdownMenuItem, IHasAccentColor
            {
                // IsHovered is used
                public override bool HandlePositionalInput => true;

                private Color4? accentColor;

                public Color4 AccentColor
                {
                    get => accentColor ?? nonAccentSelectedColor;
                    set
                    {
                        accentColor = value;
                        updateColors();
                    }
                }

                private void updateColors()
                {
                    BackgroundColourHover = accentColor ?? nonAccentHoverColor;
                    BackgroundColourSelected = accentColor ?? nonAccentSelectedColor;
                    UpdateBackgroundColour();
                    UpdateForegroundColour();
                }

                private Color4 nonAccentHoverColor;
                private Color4 nonAccentSelectedColor;

                public DrawablePiouslyDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    Foreground.Padding = new MarginPadding(2);

                    Masking = true;
                    CornerRadius = corner_radius;
                }

                [BackgroundDependencyLoader]
                private void load(PiouslyColor colors)
                {
                    BackgroundColour = Color4.Transparent;

                    nonAccentHoverColor = colors.PinkDarker;
                    nonAccentSelectedColor = Color4.Black.Opacity(0.5f);
                    updateColors();

                    AddInternal(new HoverClickSounds(HoverSampleSet.Soft));
                }

                protected override void UpdateForegroundColour()
                {
                    base.UpdateForegroundColour();

                    if (Foreground.Children.FirstOrDefault() is Content content) content.Chevron.Alpha = IsHovered ? 1 : 0;
                }

                protected override Drawable CreateContent() => new Content();

                protected new class Content : FillFlowContainer, IHasText
                {
                    public string Text
                    {
                        get => Label.Text;
                        set => Label.Text = value;
                    }

                    public readonly PiouslySpriteText Label;
                    public readonly SpriteIcon Chevron;

                    public Content()
                    {
                        RelativeSizeAxes = Axes.X;
                        AutoSizeAxes = Axes.Y;
                        Direction = FillDirection.Horizontal;

                        Children = new Drawable[]
                        {
                            Chevron = new SpriteIcon
                            {
                                AlwaysPresent = true,
                                Icon = FontAwesome.Solid.ChevronRight,
                                Colour = Color4.Black,
                                Alpha = 0.5f,
                                Size = new Vector2(8),
                                Margin = new MarginPadding { Left = 3, Right = 3 },
                                Origin = Anchor.CentreLeft,
                                Anchor = Anchor.CentreLeft,
                            },
                            Label = new PiouslySpriteText
                            {
                                Origin = Anchor.CentreLeft,
                                Anchor = Anchor.CentreLeft,
                            },
                        };
                    }
                }
            }

            #endregion
        }

        #endregion

        public class PiouslyDropdownHeader : DropdownHeader, IHasAccentColor
        {
            protected readonly SpriteText Text;

            protected override string Label
            {
                get => Text.Text;
                set => Text.Text = value;
            }

            protected readonly SpriteIcon Icon;

            private Color4 accentColor;

            public virtual Color4 AccentColor
            {
                get => accentColor;
                set
                {
                    accentColor = value;
                    BackgroundColourHover = accentColor;
                }
            }

            public PiouslyDropdownHeader()
            {
                Foreground.Padding = new MarginPadding(4);

                AutoSizeAxes = Axes.None;
                Margin = new MarginPadding { Bottom = 4 };
                CornerRadius = corner_radius;
                Height = 40;

                Foreground.Children = new Drawable[]
                {
                    Text = new PiouslySpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    Icon = new SpriteIcon
                    {
                        Icon = FontAwesome.Solid.ChevronDown,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Margin = new MarginPadding { Right = 5 },
                        Size = new Vector2(12),
                    },
                };

                AddInternal(new HoverClickSounds());
            }

            [BackgroundDependencyLoader]
            private void load(PiouslyColor colors)
            {
                BackgroundColour = Color4.Black.Opacity(0.5f);
                BackgroundColourHover = colors.PinkDarker;
            }
        }
    }
}
