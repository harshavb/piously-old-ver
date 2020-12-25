using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Piously.Game.Graphics.Containers
{
    public class HexagonGroup : Container<HexagonalContainer>
    {
        //offset variable
        //2D array representing hexagons

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Both;

            Children = new HexagonalContainer[]
            {
                new HexagonalContainer
                {

                }
            };
        }
    }
}
