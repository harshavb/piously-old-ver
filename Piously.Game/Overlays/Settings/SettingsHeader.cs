using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;

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
        private void load(PiouslyColor colors)
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
                        new PiouslySpriteText
                        {
                            Text = heading,
                            Font = PiouslyFont.GetFont(size: 40),
                            Margin = new MarginPadding
                            {
                                Left = SettingsPanel.CONTENT_MARGINS,
                                Top = Toolbar.Toolbar.TOOLTIP_HEIGHT
                            },
                        },
                        new PiouslySpriteText
                        {
                            Colour = colors.Pink,
                            Text = subheading,
                            Font = PiouslyFont.GetFont(size: 18),
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
