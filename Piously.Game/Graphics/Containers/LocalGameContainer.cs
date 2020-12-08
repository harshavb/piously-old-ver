using System;
using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics.UserInterface;
namespace Piously.Game.Graphics.Containers
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
                    new Container
                    {
                        RelativePositionAxes = Axes.Both,
                        Position = new Vector2(0.025f, 0.05f),

                        // TitleTextSprite
                        Child = new SpriteText
                        {
                            Font = new FontUsage("Aller", 64, "Bold", false, false),
                            Text = "Local Game",
                        }
                    },

                    // LeftPanelContainer
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        RelativePositionAxes = Axes.Both,
                        Size = new Vector2(0.4f, 1f),
                        Position = new Vector2(0.025f, 0f),
                        Children = new Drawable[] {

                            // CreateTextContainer
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(1f, 0.15f),
                                Position = new Vector2(0f, 0.2f),
                                Child = new SpriteText
                                {
                                    Font = new FontUsage("Aller", 48, null, false, false),
                                    Text = "Create Game",
                                }
                            },

                            // LoadTextContainer
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(1f, 0.15f),
                                Position = new Vector2(0f, 0.4f),
                                Child = new SpriteText
                                {
                                    Font = new FontUsage("Aller", 48, null, false, false),
                                    Text = "Load Saved Game",
                                }
                            }
                        }
                    },

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
                            // GameRulesContainer
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(0.33f, 0.075f),
                                Position = new Vector2(0.33f, 0.025f),
                                Child = new SpriteText
                                {
                                    Font = new FontUsage("Aller", 24, null, false, false),
                                    Text = "Game Rules",
                                }
                            },

                            // TimerTitleContainer
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(0.4f, 0.05f),
                                Position = new Vector2(0.3f, 0.15f),
                                Child = new SpriteText
                                {
                                    Font = new FontUsage("Aller", 16, null, false, false),
                                    Text = "Timer",
                                }
                            },

                            // BorderedContainer
                            new Container
                            {
                                Masking = true,
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(0.33f, 0.075f),
                                Position = new Vector2(0.4f, 0.2f),
                                BorderColour = new PiouslyColour().Gray7,
                                BorderThickness = 3,
                                Child = new PiouslyTextBox
                                {
                                    Size = new Vector2(1f),
                                    Position = new Vector2(0f),
                                    Text = "10:00",
                                    PlaceholderText = "Time per Player",
                                },
                            },

                            // SaveTitleContainer
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(0.4f, 0.05f),
                                Position = new Vector2(0.3f, 0.3f),
                                Child = new SpriteText
                                {
                                    Font = new FontUsage("Aller", 16, null, false, false),
                                    Text = "Create a new save file",
                                }
                            },

                            // BorderedContainer
                            new Container
                            {
                                Masking = true,
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(0.33f, 0.075f),
                                Position = new Vector2(0.4f, 0.35f),
                                BorderColour = new PiouslyColour().Gray7,
                                BorderThickness = 3,
                                Child = new PiouslyTextBox
                                {
                                    Size = new Vector2(1f),
                                    Position = new Vector2(0f),
                                    Text = "10:00",
                                    PlaceholderText = "Time per Player",
                                },
                            },

                            // TwoPlayerContainer
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Size = new Vector2(0.95f, 0.3f),
                                Position = new Vector2(0.025f, 0.5f),
                                Children = new Drawable[]
                                {

                                }
                            }
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
