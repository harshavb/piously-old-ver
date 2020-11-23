using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace Piously.Game.Graphics.Containers
{
    public class MenuButton : EquilateralTriangle
    {
        private SpriteText label;

        //private Action clickAction;

        public SpriteText Label
        {
            get => label;
            set
            {
                if (label == value)
                    return;

                label = value;
            }
        }

        /*public Action ClickAction
        {
            get => clickAction;
            set
            {
                if (clickAction == value)
                    return;

                clickAction = value;
            }
        }*/

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.ScaleTo(1.15f, 25);
            this.ScaleTo(1.13f, 25);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (IsHovered)
                this.ScaleTo(1.1f, 25);
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
