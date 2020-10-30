using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using Piously.Game.Graphics.Containers;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class TestScreen : Screen
    {
        private Box box = new Box();

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Margin = new MarginPadding { Top = 20 },
                Children = new Drawable[]
                {
                    box = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(150, 150),
                        Colour = Color4.Tomato
                    },
                    new TestClickableContainer
                    {
                        Child = new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(150, 150),
                            Colour = Color4.Tomato
                        },
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Both,
                    },
                    new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = "Testing font",
                        Font = new FontUsage(family: "InkFree-Bold", size: 40f)
                    }
                }
            });
        }

        public void editBoxColour(Color4 colour)
        {
            box.Colour = colour;
        }

        public void rotateBox()
        {
            box.Rotation += (float)Time.Elapsed / 10;
        }
    }
}
