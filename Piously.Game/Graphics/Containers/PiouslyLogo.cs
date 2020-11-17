using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using Piously.Game.Graphics.Shapes;
using Piously.Game.Screens.Menu;
using Piously.Game.Graphics.Backgrounds;
using osuTK;
using System;

namespace Piously.Game.Graphics.Containers
{
    public class PiouslyLogo : HexagonalContainer
    {
        public MenuState menuState;
        private const double transition_length = 300;
        private Sprite logo;
        public MenuLogo parentLogo;

        private readonly Container colourAndHexagons;

        public bool Hexagons
        {
            set => colourAndHexagons.FadeTo(value ? 1 : 0, transition_length, Easing.OutQuint);
        }

        public PiouslyLogo()
        {
            menuState = MenuState.Closed;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(800, 800);

            Children = new Drawable[]
            {
                new HexagonalContainer()
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Scale = new Vector2(0.95f),
                    Child = colourAndHexagons = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Children = new Drawable[]
                        {
                            new Hexagon
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = PiouslyColour.PiouslyLightYellow,
                            },
                            new Hexagons
                            {
                                HexagonScale = 3,
                                ColourLight = PiouslyColour.PiouslyLighterYellow,
                                ColourDark = PiouslyColour.PiouslyYellow,
                                RelativeSizeAxes = Axes.Both,
                            }
                        }
                    }
                },
                logo = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fit,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            logo.Texture = textures.Get(@"Menu/logo");
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            parentLogo.ScaleTo(1.13f, 25);
            switch (menuState)
            {
                case MenuState.Opened:
                    menuState = MenuState.Closed;
                    parentLogo.toggleButtons();
                    break;
                case MenuState.Closed:
                    menuState = MenuState.Opened;
                    parentLogo.toggleButtons();
                    break;
                default:
                    break;
            }
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (IsHovered)
                parentLogo.ScaleTo(1.1f, 25);
        }
        protected override bool OnHover(HoverEvent e)
        {
            parentLogo.closeAllTriangles();
            if(menuState == MenuState.Closed)
                parentLogo.ScaleTo(1.1f, 50);
            return false;
        }
        protected override void OnHoverLost(HoverLostEvent e)
        {
            parentLogo.checkTriangleHovers();
            if (menuState == MenuState.Closed)
                parentLogo.ScaleTo(1f, 50);
        }

    }
}
