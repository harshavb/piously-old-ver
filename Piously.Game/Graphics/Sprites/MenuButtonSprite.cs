using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using Piously.Game.Graphics.Containers;
using Piously.Game.Screens.Menu;

namespace Piously.Game.Graphics.Sprites
{
    public class MenuButtonSprite : EquilateralTriangle
    {
        public MenuLogo parentLogo;
        public Action clickAction;

        protected override bool OnClick(ClickEvent e)
        {
            trigger();
            return true;
        }

        private void trigger()
        {
            clickAction?.Invoke();
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            Parent.ScaleTo(1.3f, 25);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (IsHovered)
            {
                Parent.ScaleTo(1.25f, 25);
            }

        }
        protected override bool OnHover(HoverEvent e)
        {
            if (parentLogo.logo.menuState == MenuState.Opened && parentLogo.logo.IsHovered == false)
            {
                Parent.ScaleTo(1.25f, 50);
            }

            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (parentLogo.logo.menuState == MenuState.Opened && parentLogo.logo.IsHovered == false)
            {
                Parent.ScaleTo(1.15f, 50);
            }
        }
    }
}
