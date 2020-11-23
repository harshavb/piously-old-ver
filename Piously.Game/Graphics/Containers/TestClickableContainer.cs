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
            this.ScaleTo(0.753f, 25);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if(IsHovered)
                this.ScaleTo(0.733f, 25);
        }
    }
}
