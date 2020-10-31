using System;
using osu.Framework.Allocation;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using Piously.Game.Input.Bindings;
using Piously.Game.Screens.Menu;
using osuTK.Graphics;
using osuTK.Input;
using LogLevel = osu.Framework.Logging.LogLevel;
using osu.Framework.Input.Events;
using System.Net.Sockets;

namespace Piously.Game
{
    //The actual game, specifically loads the UI
    public class PiouslyGame : PiouslyGameBase
    {
        private ScreenStack piouslyMenuScreenStack;
        private TestScreen testScreen;
        private ScreenStack piouslyMenuScreenStack2;
        private TestScreen2 testScreen2;

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance)
            {
                Logger.Log(@"Piously does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            piouslyMenuScreenStack = new ScreenStack();
            piouslyMenuScreenStack2 = new ScreenStack();

            AddInternal(new PiouslyKeyBindingContainer
            {
                Children = new[]
                {
                    piouslyMenuScreenStack,
                    piouslyMenuScreenStack2
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            testScreen = new TestScreen();
            testScreen2 = new TestScreen2();

            piouslyMenuScreenStack.Push(testScreen);
            piouslyMenuScreenStack2.Push(testScreen2);
        }

        protected override void Update()
        {
            base.Update();
            testScreen.rotateBox();
            testScreen2.rotateText();
        }
    }
}