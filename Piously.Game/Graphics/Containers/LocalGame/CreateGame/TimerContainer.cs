using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame.CreateGame
{
    public class TimerContainer : Container
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Position = new Vector2(0f, 0.125f);
            Size = new Vector2(0.33f, 0.125f);

            Children = new Drawable[]
            {
                //Timer
                new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativePositionAxes = Axes.Both,
                    Position = Vector2.Zero,
                    Font = new FontUsage("Aller", 36, null, false, false),
                    Text = "Timer",
                },

                //BorderedContainer
                new BorderedPiouslyTextBox
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Size = new Vector2(0.5f, 0.5f),
                    Text = "10:00",
                    PlaceholderText = "None",
                    LengthLimit = 5,
                },
            };
        }
    }
}
