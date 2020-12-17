using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LocalGameContainer : Container
    {
        public Action onCreateGame;
        public Action onLoadSavedGame;
        public CreateGameContainer createGameContainer;
        public LoadGameContainer loadGameContainer;

        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            //Size = new Vector2(1250, 677);

            Children = new Drawable[]
            {
                // TitleContainer
                new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Position = new Vector2(150, 75),
                    Font = new FontUsage("Aller", 64, "Bold", false, false),
                    Text = "Local Game",
                },

                // LeftPanelContainer
                new LeftPanelContainer {
                    OnCreateGame = onCreateGame,
                    OnLoadSavedGame = onLoadSavedGame,
                },

                // CreateGameContainer
                createGameContainer = new CreateGameContainer(),

                // LoadGameContainer
                loadGameContainer = new LoadGameContainer(),
            };
        }
    }
}
