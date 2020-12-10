using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Screens.Menu;
using osuTK;

namespace Piously.Game.Graphics.Containers.MainMenu
{
    public class MainMenuContainer : Container
    {
        public PiouslyLogo logo;
        public ParallaxContainer parallaxContainer;
        public MenuSplitHexagon menuButtons;

        //private MainMenuContainerState lastState = MainMenuContainerState.Initial;

        [BackgroundDependencyLoader]
        private void load()
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
                        Size = new Vector2(0.5f),
                        parentLogo = this,
                    },
                    logo = new PiouslyLogo()
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(0.5f),
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

        public void updateState(MainMenuContainerState state = MainMenuContainerState.Initial)
        {
            switch(state)
            {
                case MainMenuContainerState.Initial:
                    this.ScaleTo(1f, 500, Easing.None);
                    this.MoveTo(new Vector2(0, 0), 500, Easing.OutQuad);
                    menuButtons.FadeTo(1, 300, Easing.None);
                    this.FadeTo(1, 300, Easing.None);
                    break;
                case MainMenuContainerState.Exit:
                    this.ScaleTo(0.5f, 500, Easing.None);
                    this.MoveTo(new Vector2(-1500, 0), 500, Easing.InQuad);
                    menuButtons.FadeTo(0, 300, Easing.None);
                    this.FadeTo(0, 300, Easing.None);
                    break;
            }
        }

    }

    public enum MainMenuContainerState
    {
        Exit,
        Initial,
    }
}
