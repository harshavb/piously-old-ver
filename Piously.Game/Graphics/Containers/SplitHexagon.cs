using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class SplitHexagon : Container<MenuButton>
    {
        public MenuLogo parentLogo;
        private float spacing = 0;
        public MenuButton[] triangles;

        /// <summary>
        /// The spacing between individual elements. Default is 0.
        /// </summary>
        public float Spacing
        {
            get => spacing;
            set
            {
                if(spacing == value)
                    return;

                spacing = value;
            }
        }

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
                    Label = new SpriteText(),
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f),
                    Rotation = i * 60 + Rotation,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.TopCentre,
                    EdgeSmoothness = new Vector2(3, 3),
                    parentLogo = parentLogo,
                });
            }
        }

        public void ScaleTo(float newScale, double duration = 0, Easing easing = Easing.None)
        {
            foreach(EquilateralTriangle triangle in Children)
            {
                triangle.ScaleTo(newScale, duration, easing);
            }
        }
    }
}
