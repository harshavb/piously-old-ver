using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using Piously.Game.Screens.Menu;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class MenuLogo : Container
    {
        public PiouslyLogo logo;
        public ParallaxContainer parallaxContainer;
        public MenuSplitHexagon menuButtons;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(1000, 1000);
            Child = parallaxContainer = new ParallaxContainer
            {
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
                }
            };
        }

        public void toggleButtons()
        {
            if(logo.menuState == MenuState.Closed)
            {
                menuButtons.ScaleTo(0.99f, 300, Easing.InOutQuint);
            }
            else if(logo.menuState == MenuState.Opened)
            {
                menuButtons.ScaleTo(1.15f, 300, Easing.InOutBounce);
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
                        menuButtons.triangles[i].ScaleTo(1.25f, 100, Easing.InOutQuint);
                    }
                }
            }
        }

        public void closeAllTriangles()
        {
            if (logo.menuState == MenuState.Opened)
            {
                menuButtons.ScaleTo(1.15f, 100, Easing.InOutBounce);
            }
        }
    }
}
