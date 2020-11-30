using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics;

namespace Piously.Game.Overlays.Settings
{
    public class SettingsHeader : Container
    {
        private readonly string heading;
        private readonly string subheading;

        public SettingsHeader(string heading, string subheading)
        {
            this.heading = heading;
            this.subheading = subheading;
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colours)
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new SpriteText
                        {
                            Text = heading,
                            Font = new FontUsage(size: 40),
                            Margin = new MarginPadding
                            {
                                Left = SettingsPanel.CONTENT_MARGINS,
                            },
                        },
                        new SpriteText
                        {
                            Colour = colours.Pink,
                            Text = subheading,
                            Font = new FontUsage(size: 18),
                            Margin = new MarginPadding
                            {
                                Left = SettingsPanel.CONTENT_MARGINS,
                                Bottom = 30
                            },
                        },
                    }
                }
            };
        }
    }
}
