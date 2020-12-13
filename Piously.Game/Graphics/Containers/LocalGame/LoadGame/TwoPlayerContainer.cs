using osu.Framework.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame.LoadGame
{
    public class TwoPlayerContainer : Container
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(1f, 0.2f);
            Position = new Vector2(0f, 0.6f);

            Children = new Drawable[]
            {
                //Player1Container
                new PlayerContainer(PlayerContainerPlayer.Player1)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(0.4f, 1f),
                    Position = new Vector2(-0.05f, 0f),
                },

                //Player2Container
                new PlayerContainer(PlayerContainerPlayer.Player2)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(0.4f, 1f),
                    Position = new Vector2(0.05f, 0f),
                },
            };
        }
    }
}
