// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics;
using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using Piously.Game.Graphics.Sprites;

namespace Piously.Game.Overlays.Comments
{
    public class TotalCommentsCounter : CompositeDrawable
    {
        public readonly BindableInt Current = new BindableInt();

        private PiouslySpriteText counter;

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider)
        {
            RelativeSizeAxes = Axes.X;
            Height = 50;
            AddInternal(new FillFlowContainer
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Margin = new MarginPadding { Left = 50 },
                Spacing = new Vector2(5, 0),
                Children = new Drawable[]
                {
                    new PiouslySpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Font = PiouslyFont.GetFont(size: 20, italics: true),
                        Colour = colorProvider.Light1,
                        Text = @"Comments"
                    },
                    new CircularContainer
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        AutoSizeAxes = Axes.Both,
                        Masking = true,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = colorProvider.Background6
                            },
                            counter = new PiouslySpriteText
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Margin = new MarginPadding { Horizontal = 10, Vertical = 5 },
                                Font = PiouslyFont.GetFont(size: 14, weight: FontWeight.Bold),
                                Colour = colorProvider.Foreground1
                            }
                        },
                    }
                }
            });
        }

        protected override void LoadComplete()
        {
            Current.BindValueChanged(value => counter.Text = value.NewValue.ToString("N0"), true);
            base.LoadComplete();
        }
    }
}
