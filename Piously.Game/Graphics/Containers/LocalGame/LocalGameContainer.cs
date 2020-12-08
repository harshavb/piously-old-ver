using System;
using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Graphics.Containers.LocalGame.CreateGame;
using Piously.Game.Graphics.Containers.LocalGame.LoadGame;
namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LocalGameContainer : Container
    {
        public LocalGameContainer()
        {
            Alpha = 0;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(1500, 900);

            // ContentContainer
            Child = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1f),

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
                    new LeftPanelContainer(),

                    // MainContentContainer
                    new Container
                    {
                        Masking = true,
                        RelativeSizeAxes = Axes.Both,
                        RelativePositionAxes = Axes.Both,
                        Size = new Vector2(0.45f, 0.85f),
                        Position = new Vector2(0.5f, 0.1f),
                        EdgeEffect = new EdgeEffectParameters {
                            Type = EdgeEffectType.Shadow,
                            Colour = Colour4.Black,
                            Radius = 10,
                            Roundness = 0.6f,
                        },
                        Children = new Drawable[]
                        {
                            // Background
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(1f),
                                Colour = new Colour4(0.2f, 0.2f, 0.2f, 0.4f),
                            },

                            // GameRules
                            new SpriteText
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Position = new Vector2(0.33f, 0.025f),
                                Font = new FontUsage("Aller", 24, null, false, false),
                                Text = "Game Rules",
                            },

                            // TimerContainer
                            new TimerContainer(),

                            // SaveNameContainer
                            new SaveNameContainer(),

                            // TwoPlayerContainer
                            new CreateGame.TwoPlayerContainer(),

                            // StartButtonContainer
                            new StartButtonContainer(),
                        }
                    }
                }
            };
        }

        public void updateState(LocalGameContainerState state = LocalGameContainerState.Initial)
        {
            switch (state)
            {
                case LocalGameContainerState.Initial:
                    this.ScaleTo(1f, 500, Easing.None);
                    this.FadeTo(1, 300, Easing.None);
                    Console.WriteLine("Making bigger and visbler");
                    break;
                case LocalGameContainerState.Exit:
                    this.ScaleTo(0.5f, 500, Easing.None);
                    this.FadeTo(0, 300, Easing.None);
                    break;
            }
        }
    }

    public enum LocalGameContainerState
    {
        Exit,
        Initial,
    }
        
}
