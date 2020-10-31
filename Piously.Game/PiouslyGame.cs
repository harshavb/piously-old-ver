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

namespace Piously.Game
{
    //The actual game, specifically loads the UI
    public class PiouslyGame : PiouslyGameBase, IKeyBindingHandler<GlobalAction>
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

            Child = piouslyMenuScreenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
            testScreen = new TestScreen();
            testScreen2 = new TestScreen2();

            Add(piouslyMenuScreenStack = new ScreenStack());
            Add(piouslyMenuScreenStack2 = new ScreenStack());
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

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.A)
                Logger.Log("Detected A");
            if(e.Key == Key.F)
                Logger.Log("Detected F");
            if (e.Key == Key.ControlLeft)
                Logger.Log("Detected ControlLeft");

            return base.OnKeyDown(e);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.TestAction1:
                    testScreen.editBoxColour(Color4.GreenYellow);
                    Logger.Log("TestAction1 detected");
                    break;
                case GlobalAction.TestAction2:
                    testScreen.editBoxColour(Color4.HotPink);
                    Logger.Log("TestAction2 detected");
                    break;
            }

            return false;
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
    }
}