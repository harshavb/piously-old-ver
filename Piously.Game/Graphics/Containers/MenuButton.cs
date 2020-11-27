using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using Piously.Game.Screens.Menu;

namespace Piously.Game.Graphics.Containers
{
    public class MenuButton : EquilateralTriangle
    {
        public MenuLogo parentLogo;

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
            if (parentLogo.logo.menuState == MenuState.Opened && parentLogo.logo.IsHovered == false)
                this.ScaleTo(1.51f, 50);

            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (parentLogo.logo.menuState == MenuState.Opened && parentLogo.logo.IsHovered == false)
                this.ScaleTo(1.375f, 50);
        }
    }
}
