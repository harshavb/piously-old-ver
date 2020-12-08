using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using Piously.Game.Graphics.Containers.MainMenu;
using Piously.Game.Screens.Menu;

namespace Piously.Game.Graphics.Shapes
{
    public class MenuTriangle : EquilateralTriangle
    {
        public MainMenuContainer parentLogo;
        protected override bool OnHover(HoverEvent e)
        {
            if (parentLogo.logo.menuState == MenuState.Opened && !parentLogo.logo.IsHovered)
            {
                this.ScaleTo(1.5f, 100, Easing.InOutBounce);
            }
            return false;
        }
        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (parentLogo.logo.menuState == MenuState.Opened)
            {
                this.ScaleTo(1.375f, 100, Easing.InOutBounce);
            }
        }
    }
}
