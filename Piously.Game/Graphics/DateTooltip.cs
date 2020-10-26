using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics.Sprites;
using osuTK;

namespace Piously.Game.Graphics
{
    public class DateTooltip : VisibilityContainer, ITooltip
    {
        private readonly PiouslySpriteText dateText, timeText;
        private readonly Box background;

        public DateTooltip()
        {
            AutoSizeAxes = Axes.Both;
            Masking = true;
            CornerRadius = 5;

            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both
                },
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Padding = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        dateText = new PiouslySpriteText
                        {
                            Font = PiouslyFont.GetFont(size: 12, weight: FontWeight.Bold),
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                        },
                        timeText = new PiouslySpriteText
                        {
                            Font = PiouslyFont.GetFont(size: 12, weight: FontWeight.Regular),
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                        }
                    }
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            background.Colour = colors.GreySeafoamDarker;
            timeText.Colour = colors.BlueLighter;
        }

        protected override void PopIn() => this.FadeIn(200, Easing.OutQuint);
        protected override void PopOut() => this.FadeOut(200, Easing.OutQuint);

        public bool SetContent(object content)
        {
            if (!(content is DateTimeOffset date))
                return false;

            dateText.Text = $"{date:d MMMM yyyy} ";
            timeText.Text = $"{date:HH:mm:ss \"UTC\"z}";
            return true;
        }

        public void Move(Vector2 pos) => Position = pos;
    }
}
