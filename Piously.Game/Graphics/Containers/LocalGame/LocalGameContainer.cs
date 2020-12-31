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
            HexagonalContainer[,] hexagons = new HexagonalContainer[20, 20];

            for(int i = 0; i < hexagons.GetLength(0); i++)
            {
                for(int j = 0; j < hexagons.GetLength(1); j++)
                {
                    hexagons[i, j] = new HexagonalContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Size = new Vector2(200, 200),
                        Colour = PiouslyColour.PiouslyLightYellow,
                    };
                }
            }

            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.9f, 1f);
            //Size = new Vector2(1250, 677);

            Children = new Drawable[]
            {
                new HexagonGroup
                {
                    Hexagons = hexagons,
                },

                // TitleContainer
                new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Position = new Vector2(150, 100),
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
