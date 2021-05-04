using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Testing;
using osuTK;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Backgrounds;
using Piously.Game.Graphics.Containers;

namespace Piously.MenuTests.Visual
{
    public class TestSceneHexagonContainer : TestScene
    {
        private readonly Container container;
        private readonly HexagonalContainer hexagonalContainer;
        private readonly ToggleableHexagons hexagons;

        public TestSceneHexagonContainer()
        {
            AddRange(new Drawable[]
            {
                container = new Container
                {
                    Size = new Vector2(256),
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
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Colour4.Aqua
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
                        }
                    }
                }
            });

            AddSliderStep(@"Resize", 64, 768, 256, value => container.ResizeTo(value));
            AddSliderStep(@"Rotate", 0, 360, 0, value => hexagonalContainer.RotateTo(value));
            AddToggleStep(@"HexagonsEffectVisibility", value => hexagons.ToggleVisibility());
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
