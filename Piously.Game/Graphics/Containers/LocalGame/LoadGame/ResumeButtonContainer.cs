using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.Backgrounds;
using Piously.Game.Graphics.UserInterface;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame.LoadGame
{
    public class ResumeButtonContainer : CircularContainer
    {
        [BackgroundDependencyLoader]
        private void load(PiouslyColour colour)
        {
            Anchor = Anchor.BottomCentre;
            Origin = Anchor.BottomCentre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(0.6f, 0.1f);
            Position = new Vector2(0f, -0.025f);
            Masking = true;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = colour.Yellow,
                },

                new Hexagons
                {
                    HexagonScale = 2,
                    ColourLight = colour.YellowDark,
                    ColourDark = colour.YellowLight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f, 3f),
                },

                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = new FontUsage("Aller", 40, "Bold", false, false),
                    Text = "RESUME",
                },

                new PiouslyButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = new Colour4(0, 0, 0, 0),
                }
            };
        }
    }
}
