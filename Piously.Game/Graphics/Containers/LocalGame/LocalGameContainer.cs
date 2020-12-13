using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
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
            Size = new Vector2(1000, 650);

            Children = new Drawable[]
            {
                // BackgroundDrawing
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = new Colour4(0.25f, 0.25f, 0.25f, 0.4f),
                },

                // TitleContainer
                new TitleContainer(),

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
