using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using Piously.Game.Graphics.Containers;

namespace Piously.Game.Graphics.Containers
{
    public class TestHoverableContainer : HexagonalContainer
    {
        public TestHoverableContainer()
            : base()
        {
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(0.733f, 50);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(0.667f, 50);
        }
    }
}
