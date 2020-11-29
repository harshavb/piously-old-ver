using System;
using osu.Framework.Input.Events;
using osu.Framework.Graphics;
using Piously.Game.Screens.Menu;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class MenuLogo : HexagonalContainer
    {
        public PiouslyLogo logo;
        public MenuSplitHexagon menuButtons;

        public MenuLogo()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(1333, 1333);

            Children = new Drawable[]
            {
                menuButtons = new MenuSplitHexagon
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1f * 0.5f),
                    parentLogo = this,
                },
                logo = new PiouslyLogo()
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1f * 0.5f),
                    parentLogo = this,
                },
            };
        }

        public void toggleButtons()
        {
            if(logo.menuState == MenuState.Closed)
            {
                menuButtons.ScaleTo(0.99f, 300, Easing.InOutBounce);
            }
            else if(logo.menuState == MenuState.Opened)
            {
                menuButtons.ScaleTo(1.375f, 300, Easing.InOutBounce);
            }
        }

        public void checkTriangleHovers()
        {
            if (logo.menuState == MenuState.Opened)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (menuButtons.triangles[i].menuButtonSprite.IsHovered)
                    {
                        menuButtons.triangles[i].ScaleTo(1.51f, 100, Easing.InOutBounce);
                    }
                }
            }
        }

        public void closeAllTriangles()
        {
            if (logo.menuState == MenuState.Opened)
            {
                menuButtons.ScaleTo(1.375f, 100, Easing.InOutBounce);
            }
        }
    }
}
