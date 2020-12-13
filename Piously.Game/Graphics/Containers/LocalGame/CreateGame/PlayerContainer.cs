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
                new BorderedPiouslyTextBox
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(1f, 0.2f),
                    Text = "Player 1",
                    PlaceholderText = "Player 1 Name",
                    LengthLimit = 16,
                    BorderColour = Colour4.LightGray,
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
                    Size = new Vector2(0.375f, 0.35f),
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
                    Font = new FontUsage("Aller", 20, "Bold", false, false),
                    Text = player == PlayerContainerPlayer.Player1 ? "Goes first" : "Creates the board",
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
