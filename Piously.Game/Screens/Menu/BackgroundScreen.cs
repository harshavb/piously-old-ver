using osu.Framework.Allocation;
using osu.Framework.Screens;
using Piously.Game.Screens.Backgrounds;
using System;

namespace Piously.Game.Screens.Menu
{
    public class BackgroundScreen : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Random randomGenerator = new Random();
            int choice = randomGenerator.Next(1, 18);
            AddInternal(new Background("Menu/menu-background-" + choice));
        }
    }
}
