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
                new Container
                {
                    Masking = true,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(0.4f, 0.5f),
                    BorderThickness = 20,
                    BorderColour = new PiouslyColour().Gray9,

                    Child = new PiouslyTextBox
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativePositionAxes = Axes.Both,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1f),
                        Text = "10:00",
                        PlaceholderText = "None",
                        LengthLimit = 5,
                        
                    }
                }
            };
        }
    }
}
