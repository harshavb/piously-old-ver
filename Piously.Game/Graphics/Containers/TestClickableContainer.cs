using osu.Framework.Graphics;
using osu.Framework.Input.Events;

namespace Piously.Game.Graphics.Containers
{
    public class TestClickableContainer : TestHoverableContainer
    {
        public TestClickableContainer()
            : base()
        {
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.ScaleTo(1.15f, 25);
            this.ScaleTo(1.13f, 25);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            this.ScaleTo(1.1f, 25);
        }
    }
}
