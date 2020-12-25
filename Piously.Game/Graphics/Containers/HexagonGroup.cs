using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class HexagonGroup : Container<HexagonalContainer>
    {
        //offset variable
        private Vector2 offset;

        public Vector2 Offset
        {
            get => offset;
            set
            {
                if (offset == value) return;

                offset = value;
            }
        }

        //spacing variable
        private Vector2 spacing;

        public Vector2 Spacing
        {
            get => offset;
            set
            {
                if (offset == value) return;

                offset = value;
            }
        }

        //2D array representing hexagons, the length of each dimension representing how many hexagons should exist.
        private bool[][] hexagons;

        public bool[][] Hexagons
        {
            get => hexagons;
            set
            {
                if (hexagons == value) return;

                hexagons = value;
            }
        }

        private float hexagonHeights;

        //We want the amount of hexagons to not affect the size of the total HexagonGroup.
        //Hexagons will be oriented with a flat side on the bottom by default.

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;



            Children = new HexagonalContainer[]
            {
                new HexagonalContainer
                {

                }
            };
        }
    }
}
