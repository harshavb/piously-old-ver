using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class MenuSplitHexagon : Container<MenuButton>
    {
        public MenuLogo parentLogo;
        public MenuButton[] triangles;

        [BackgroundDependencyLoader]
        private void load()
        {
            CreateTriangles();
        }

        private void CreateTriangles()
        {
            triangles = new MenuButton[6];
            for (int i = 0; i < 6; ++i)
            {
                Add(triangles[i] = new MenuButton
                {
                    Colour = i switch
                    {
                        0 => PiouslyColour.PiouslyDarkYellow,
                        1 => PiouslyColour.PiouslyLighterYellow,
                        2 => PiouslyColour.PiouslyDarkYellow,
                        3 => PiouslyColour.PiouslyLighterYellow,
                        4 => PiouslyColour.PiouslyDarkYellow,
                        5 => PiouslyColour.PiouslyLighterYellow
                    },
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f),
                    Scale = new Vector2(0.99f),
                    Rotation = i * 60 + Rotation,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.TopCentre,
                    parentLogo = parentLogo
                });
            }
        }

        public void ScaleTo(float newScale, double duration = 0, Easing easing = Easing.None)
        {
            foreach(MenuButton menuButton in Children)
            {
                menuButton.ScaleTo(newScale, duration, easing);
            }
        }
    }
}
