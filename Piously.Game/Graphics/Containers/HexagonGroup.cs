using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Piously.Game.Graphics.Containers
{
    public class HexagonGroup : Container<HexagonalContainer>
    {
        //offset variable
        //2D array representing hexagons, the length of each dimension representing how many hexagons should exist.
        //We want the amount of hexagons to not affect the size of the total HexagonGroup

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
