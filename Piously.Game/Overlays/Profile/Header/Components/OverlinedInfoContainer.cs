using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using osuTK.Graphics;

namespace Piously.Game.Overlays.Profile.Header.Components
{
    public class OverlinedInfoContainer : CompositeDrawable
    {
        private readonly Circle line;
        private readonly PiouslySpriteText title;
        private readonly PiouslySpriteText content;

        public string Title
        {
            set => title.Text = value;
        }

        public string Content
        {
            set => content.Text = value;
        }

        public Color4 LineColour
        {
            set => line.Colour = value;
        }

        public OverlinedInfoContainer(bool big = false, int minimumWidth = 60)
        {
            AutoSizeAxes = Axes.Both;
            InternalChild = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    line = new Circle
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 2,
                        Margin = new MarginPadding { Bottom = 2 }
                    },
                    title = new PiouslySpriteText
                    {
                        Font = PiouslyFont.GetFont(size: big ? 14 : 12, weight: FontWeight.Bold)
                    },
                    content = new PiouslySpriteText
                    {
                        Font = PiouslyFont.GetFont(size: big ? 40 : 18, weight: FontWeight.Light)
                    },
                    new Container // Add a minimum size to the FillFlowContainer
                    {
                        Width = minimumWidth,
                    }
                }
            };
        }
    }
}
