using System;

namespace Piously.Game.Screens.Backgrounds
{
    public class MainMenuBackground : BackgroundScreen
    {
        public MainMenuBackground(bool animateOnEnter = true) : base(animateOnEnter, "Menu/menu-background-" + new Random().Next(1, 18))
        { }
    }
}
