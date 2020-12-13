using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Piously.Game.Graphics.UserInterface
{
    public class BorderedPiouslyTextBox : Container
    {
        public string Text = "Text";
        public string PlaceholderText = "Placeholder Text";
        public int LengthLimit = 255;
        public PiouslyTextBox TextBox;

        public BorderedPiouslyTextBox()
        {
            BorderColour = Colour4.LightSlateGray;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            BorderThickness = 3f;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = Colour4.Transparent,
                },

                TextBox = new PiouslyTextBox
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativePositionAxes = Axes.Both,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Height = 0.9f,
                    Text = Text,
                    PlaceholderText = PlaceholderText,
                    LengthLimit = LengthLimit,
                    Colour = new PiouslyColour().GrayC,
                }
            };
        }
    }
}
