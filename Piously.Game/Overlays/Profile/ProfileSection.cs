using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Backgrounds;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Users;

namespace Piously.Game.Overlays.Profile
{
    public abstract class ProfileSection : Container
    {
        public abstract string Title { get; }

        public abstract string Identifier { get; }

        private readonly FillFlowContainer content;
        private readonly Box background;
        private readonly Box underscore;

        protected override Container<Drawable> Content => content;

        public readonly Bindable<User> User = new Bindable<User>();

        protected ProfileSection()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new SectionTriangles
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            AutoSizeAxes = Axes.Both,
                            Margin = new MarginPadding
                            {
                                Horizontal = UserProfileOverlay.CONTENT_X_MARGIN,
                                Top = 15,
                                Bottom = 20,
                            },
                            Children = new Drawable[]
                            {
                                new PiouslySpriteText
                                {
                                    Text = Title,
                                    Font = PiouslyFont.GetFont(size: 20, weight: FontWeight.Bold),
                                },
                                underscore = new Box
                                {
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.TopCentre,
                                    Margin = new MarginPadding { Top = 4 },
                                    RelativeSizeAxes = Axes.X,
                                    Height = 2,
                                }
                            }
                        },
                        content = new FillFlowContainer
                        {
                            Direction = FillDirection.Vertical,
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                            Padding = new MarginPadding
                            {
                                Horizontal = UserProfileOverlay.CONTENT_X_MARGIN,
                                Bottom = 20
                            }
                        },
                    },
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider)
        {
            background.Colour = colorProvider.Background5;
            underscore.Colour = colorProvider.Highlight1;
        }

        private class SectionTriangles : Container
        {
            private readonly Hexagons hexagons;
            private readonly Box foreground;

            public SectionTriangles()
            {
                RelativeSizeAxes = Axes.X;
                Height = 100;
                Masking = true;
                Children = new Drawable[]
                {
                    hexagons = new Hexagons
                    {
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        RelativeSizeAxes = Axes.Both,
                        HexagonScale = 3,
                    },
                    foreground = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load(OverlayColorProvider colorProvider)
            {
                hexagons.ColorLight = colorProvider.Background4;
                hexagons.ColorDark = colorProvider.Background5.Darken(0.2f);
                foreground.Colour = ColourInfo.GradientVertical(colorProvider.Background5, colorProvider.Background5.Opacity(0));
            }
        }
    }
}
