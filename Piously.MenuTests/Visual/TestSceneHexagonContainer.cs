using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Backgrounds;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Shapes;

namespace Piously.MenuTests.Visual
{
    public class TestSceneHexagonContainer : TestScene
    {
        private readonly Container container;
        private readonly HexagonalContainer hexagonalContainer;
        private readonly ToggleableHexagons hexagons;
        private readonly Hexagon hexagon;
        private readonly Triangle triangle;

        public TestSceneHexagonContainer()
        {
            Add(container = new Container
            {
                Size = new Vector2(256),
                RelativePositionAxes = Axes.Both,
                Position = new Vector2(0.5f, 0.5f),
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Red
                    },
                    hexagonalContainer = new HexagonalContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        BorderColour = Colour4.Lime,
                        BorderThickness = 2,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = Colour4.Aqua,
                                Size = new Vector2(1.0f, 1.0f)
                            },
                            hexagons = new ToggleableHexagons
                            {
                                hexagons = new Hexagons
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    ColourDark = PiouslyColour.PiouslyYellow,
                                    ColourLight = PiouslyColour.PiouslyLighterYellow
                                }
                            }
                        }
                    },
                    hexagon = new Hexagon
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Purple,
                    },
                    triangle = new Triangle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.GreenYellow,
                    }
                }
            });

            AddSliderStep(@"Resize", 64, 768, 256, value => container.ResizeTo(value));
            AddSliderStep(@"Rotate Hexagon", 0, 360, 0, value => hexagon.RotateTo(value));
            AddSliderStep(@"Rotate Triangle", 0, 360, 0, value => triangle.RotateTo(value));
            AddSliderStep(@"Rotate Container", 0, 360, 0, value => hexagonalContainer.RotateTo(value));
            AddToggleStep(@"Hexagons Effect Visibility", value => hexagons.ToggleVisibility());
        }
    }

    // Does not work as intended :)
    public class ToggleableHexagons : VisibilityContainer
    {
        public Hexagons hexagons;

        override protected void PopIn()
        {
            hexagons.Show();
        }

        override protected void PopOut()
        {
            hexagons.Hide();
        }
    }
}
