using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Framework.Screens;
using Piously.Game.Graphics.Containers;
using Piously.Game.Input.Bindings;
using Piously.Game.Screens.Menu;
using osuTK;
using osuTK.Graphics;
using LogLevel = osu.Framework.Logging.LogLevel;
using osuTK.Graphics.ES20;
using osu.Framework.Graphics.Containers;

namespace Piously.Game
{
    //The actual game, specifically loads the UI
    public class PiouslyGame : PiouslyGameBase, IKeyBindingHandler<GlobalAction>
    {
        private ScreenStack piouslyMenuScreenStack;
        private TestScreen testScreen;
        private TestScreen2 testScreen2;

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance)
            {
                Logger.Log(@"Piously does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            testScreen = new TestScreen();
            testScreen2 = new TestScreen2();

            Add(piouslyMenuScreenStack = new ScreenStack());

            piouslyMenuScreenStack.Push(testScreen);
            //testScreen.Push(testScreen2);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.TestAction1:
                    testScreen.editBoxColour(Color4.GreenYellow);
                    break;
                case GlobalAction.TestAction2:
                    testScreen.editBoxColour(Color4.HotPink);
                    break;
            }
            return true;
        }

        public void OnReleased(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.TestAction1:
                    testScreen.editBoxColour(Color4.Tomato);
                    break;
                case GlobalAction.TestAction2:
                    testScreen.editBoxColour(Color4.Tomato);
                    break;
            }
        }

        protected override void Update()
        {
            base.Update();
            testScreen.rotateBox();
            //testScreen2.rotateText();
        }
    }
}