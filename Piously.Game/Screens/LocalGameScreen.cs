using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Piously.Game.Screens.Backgrounds;
using Piously.Game.Graphics.Containers.LocalGame;

namespace Piously.Game.Screens
{
    public class LocalGameScreen : BackgroundScreen
    {
        private LocalGameContainer localGameContainer;

        public LocalGameScreen(bool animateOnEnter = true) : base(animateOnEnter, "Menu/load-game-background")
        {
            Alpha = 0;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;

            // ContentContainer
            AddInternal(localGameContainer = new LocalGameContainer()
            {
                onCreateGame = new Action(onCreateGame),
                onLoadSavedGame = new Action(onLoadSavedGame),
            });
        }

        public override void OnEntering(IScreen last)
        {
            this.ScaleTo(1f, 500, Easing.None);
            this.FadeTo(1, 300, Easing.None);
        }

        public override bool OnExiting(IScreen last)
        {
            this.ScaleTo(0.5f, 500, Easing.None);
            this.FadeTo(0, 300, Easing.None);
            return false;
        }

        public override void OnResuming(IScreen last)
        {
            this.ScaleTo(1f, 500, Easing.None);
            this.FadeTo(1, 300, Easing.None);
        }

        public override void OnSuspending(IScreen last)
        {
            this.ScaleTo(0.5f, 500, Easing.None);
            this.FadeTo(0, 300, Easing.None);
        }

        private void onCreateGame()
        {
            if (!localGameContainer.createGameContainer.isVisible) localGameContainer.createGameContainer.ToggleVisibility();
            if (localGameContainer.loadGameContainer.isVisible) localGameContainer.loadGameContainer.ToggleVisibility();
        }

        private void onLoadSavedGame()
        {
            if (localGameContainer.createGameContainer.isVisible) localGameContainer.createGameContainer.ToggleVisibility();
            if (!localGameContainer.loadGameContainer.isVisible) localGameContainer.loadGameContainer.ToggleVisibility();
        }
    }
}
