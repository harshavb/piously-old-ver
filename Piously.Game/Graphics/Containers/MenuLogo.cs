using osu.Framework.Input.Events;
using osu.Framework.Graphics;
using Piously.Game.Graphics.Shapes;
using Piously.Game.Screens.Menu;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class MenuLogo : HexagonalContainer
    {
        public PiouslyLogo logo;
        public SplitHexagon menuButtons;

        public MenuLogo()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(1200, 1200);

            Children = new Drawable[]
            {
                menuButtons = new SplitHexagon
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1f * 0.667f)
                },
                logo = new PiouslyLogo()
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1f * 0.667f),
                    parentLogo = this,
                },
            };
        }

        public void toggleButtons()
        {
            if(logo.menuState == MenuState.Closed)
            {
                menuButtons.ScaleTo(1f, 300, Easing.InOutBounce);
            }
            else if(logo.menuState == MenuState.Opened)
            {
                menuButtons.ScaleTo(1.375f, 300, Easing.InOutBounce);
            }
        }

        public void checkTriangleHovers()
        {
        }

        public void closeAllTriangles()
        {
            if (logo.menuState == MenuState.Opened)
            {
                menuButtons.ScaleTo(1.375f, 100, Easing.InOutBounce);
            }
        }

        protected override bool OnHover(HoverEvent e)
        {
            if(logo.menuState == MenuState.Opened)
                this.ScaleTo(1.1f, 50);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if(logo.menuState == MenuState.Opened)
                this.ScaleTo(1f, 50);
        }

    }
}
