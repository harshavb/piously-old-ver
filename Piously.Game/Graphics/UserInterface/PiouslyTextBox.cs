using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics;
using osuTK;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyTextBox : BasicTextBox
    {
        public PiouslyTextBox() : base()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            BackgroundFocused = Colour4.Transparent;
            BackgroundUnfocused = Colour4.Transparent;
            Masking = false;
            TextContainer.Anchor = Anchor.Centre;
            TextContainer.Origin = Anchor.Centre;
            TextContainer.Position = Vector2.Zero;
            Placeholder.Colour = PiouslyColour.PiouslyYellow;
        }

        protected override void Update()
        {
            base.Update();
            TextContainer.Position = Vector2.Zero;
        }
    }
}
