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

        public TestSceneHexagonContainer()
        {
            this.AddRange(new Drawable[]
            {
                this.container = new Container
                {
                    Size = new Vector2(256),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Red
                        },
                        new TestHexagonalContainer
                        {
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                }
            });

            this.AddSliderStep(@"Resize", 64, 768, 256, value => this.container.ResizeTo(value));
        }

        private class TestHexagonalContainer : HexagonalContainer
        {
            private readonly Box backgroundBox;

            public TestHexagonalContainer()
            {
                this.AddRange(new Drawable[]
                {
                    this.backgroundBox = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = PiouslyColour.PiouslyLightYellow
                    },
                    new Hexagons
                    {
                        RelativeSizeAxes = Axes.Both,
                        ColourDark = PiouslyColour.PiouslyYellow,
                        ColourLight = PiouslyColour.PiouslyLighterYellow
                    }
                });
            }

            protected override bool OnHover(HoverEvent e)
            {
                this.backgroundBox.FadeColour(Colour4.Black, 80);
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e) => this.backgroundBox.FadeColour(PiouslyColour.PiouslyLightYellow, 80);
        }
    }
}
