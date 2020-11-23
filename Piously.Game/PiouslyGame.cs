using System;
using osu.Framework.Allocation;
using osu.Framework.Logging;
using osu.Framework.Screens;
using Piously.Game.Input.Bindings;
using Piously.Game.Screens.Menu;
using LogLevel = osu.Framework.Logging.LogLevel;

namespace Piously.Game
{
    //The actual game, specifically loads the UI
    public class PiouslyGame : PiouslyGameBase
    {
        private ScreenStack mainMenuScreenStack;
        private MainMenu mainMenu;
        private ScreenStack backgroundStack;
        private BackgroundScreen background;

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance)
            {
                Logger.Log(@"Piously does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            backgroundStack = new ScreenStack();
            mainMenuScreenStack = new ScreenStack();

            AddInternal(new PiouslyKeyBindingContainer
            {
                Children = new[]
                {
                    backgroundStack,
                    mainMenuScreenStack,
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            background = new BackgroundScreen();
            mainMenu = new MainMenu();

            backgroundStack.Push(background);
            mainMenuScreenStack.Push(mainMenu);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}