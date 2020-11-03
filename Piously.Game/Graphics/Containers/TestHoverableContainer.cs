using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace Piously.Game.Graphics.Containers
{
    public class TestHoverableContainer : Container
    {
        public TestHoverableContainer()
            : base()
        {
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(1.1f, 50);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(1.00f, 50);
        }
    }
}
