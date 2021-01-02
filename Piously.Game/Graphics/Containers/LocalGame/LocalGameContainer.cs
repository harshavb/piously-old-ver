using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.Shapes;
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
        private void load(PiouslyColour colour)
        {
            HexagonalContainer[,] hexagons = new HexagonalContainer[10, 10];

            for(int i = 0; i < hexagons.GetLength(0); i++)
            {
                for(int j = 0; j < hexagons.GetLength(1); j++)
                {
                    hexagons[i, j] = new HexagonalContainer
                    {
                        Masking = true,
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.Centre,
                        Size = new Vector2(200, 200),
                        Children = new Drawable[]
                        {
                            new Hexagon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Colour = PiouslyColour.PiouslyYellow,
                            },
                            new HexagonalContainer
                            {
                                RelativeSizeAxes = Axes.Both,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Scale = new Vector2(0.95f),
                                Colour = PiouslyColour.Gray(33),

                                Child = new Hexagon
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                }
                            }
                        },
                        EdgeEffect = new EdgeEffectParameters
                        {
                            Type = EdgeEffectType.Shadow,
                            Colour = colour.Gray1,
                            Radius = 10,
                            Roundness = 0.6f,
                        },
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
                    RelativePositionAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Hexagons = hexagons,
                    Connected = true,
                    Position = new Vector2(0.5f, -0.4f),
                },

                // TitleContainer
                new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.Centre,
                    Position = new Vector2(300, 150),
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
