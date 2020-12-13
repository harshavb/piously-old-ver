using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Effects;
using Piously.Game.Graphics.UserInterface;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame.LoadGame
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
                // PlayerNameText
                new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0.2f, 0.1f),
                    Font = new FontUsage("Aller", 24, null, false, false),
                    Text = player == PlayerContainerPlayer.Player1 ? "Player 1" : "Player 2",
                },

                //PlayerSpriteText
                new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0.25f, 0.1f),
                    Font = new FontUsage("Aller", 24, "Bold", false, false),
                    Text = player == PlayerContainerPlayer.Player1 ? "P1" : "P2",
                },

                //CircularBlackWhiteContainer
                new CircularContainer
                {
                    Masking = true,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativePositionAxes = Axes.Both,
                    RelativeSizeAxes = Axes.Both,
                    Position = new Vector2(0.05f, 0.1f),
                    Size = new Vector2(0.12f, 0.2f),
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
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0f, -0.05f),
                    Font = new FontUsage("Aller", 20, "Bold", false, false),
                    Text = player == PlayerContainerPlayer.Player1 ? "Went first" : "Created the board",
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
