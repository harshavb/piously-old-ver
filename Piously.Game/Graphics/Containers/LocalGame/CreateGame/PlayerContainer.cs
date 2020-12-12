using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Effects;
using Piously.Game.Graphics.UserInterface;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame.CreateGame
{
    public class PlayerContainer : Container
    {
        private readonly PlayerContainerPlayer player;
        
        public PlayerContainer(PlayerContainerPlayer player)
        {
            this.player = player;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = new PiouslyColour().Gray1,
                Radius = 10,
                Roundness = 0.6f,
            };

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = new PiouslyColour().Gray7,
                },
                // PlayerNameTextBox
                new Container
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f, 0.2f),
                    Masking = true,
                    BorderThickness = 20,
                    BorderColour = new PiouslyColour().Gray9,

                    Child = new PiouslyTextBox
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativePositionAxes = Axes.Both,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.95f),
                        Text = player == PlayerContainerPlayer.Player1 ? "Player 1" : "Player 2",
                        PlaceholderText = "Enter player name",
                        LengthLimit = 20,
                    }
                },

                //PlayerSpriteText
                new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0f, 0.2f),
                    Font = new FontUsage("Aller", 32, "Bold", false, false),
                    Text = player == PlayerContainerPlayer.Player1 ? "Player 1" : "Player 2",
                },

                //CircularBlackWhiteContainer
                new CircularContainer
                {
                    Masking = true,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativePositionAxes = Axes.Both,
                    RelativeSizeAxes = Axes.Both,
                    Position = new Vector2(0f, 0.1f),
                    Size = new Vector2(0.3f, 0.343f),
                    Child = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1f),
                        Colour = player == PlayerContainerPlayer.Player1 ? Colour4.White : Colour4.Black,
                    }
                },

                //PlayerTraitText
                new SpriteText
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = new FontUsage("Aller", 26, "Bold", false, false),
                    Text = player == PlayerContainerPlayer.Player1 ? "Goes first" : "Creates the board",
                    MaxWidth = 250,
                }
            };
        }
    }

    public enum PlayerContainerPlayer
    {
        Player1,
        Player2
    }
}
