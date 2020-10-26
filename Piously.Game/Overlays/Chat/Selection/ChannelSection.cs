using System;
using System.Collections.Generic;
using System.Linq;
using osuTK;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Online.Chat;

namespace Piously.Game.Overlays.Chat.Selection
{
    public class ChannelSection : Container, IHasFilterableChildren
    {
        private readonly PiouslySpriteText header;

        public readonly FillFlowContainer<ChannelListItem> ChannelFlow;

        public IEnumerable<IFilterable> FilterableChildren => ChannelFlow.Children;
        public IEnumerable<string> FilterTerms => Array.Empty<string>();

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1f : 0f, 100);
        }

        public bool FilteringActive { get; set; }

        public string Header
        {
            get => header.Text;
            set => header.Text = value.ToUpperInvariant();
        }

        public IEnumerable<Channel> Channels
        {
            set => ChannelFlow.ChildrenEnumerable = value.Select(c => new ChannelListItem(c));
        }

        public ChannelSection()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                header = new PiouslySpriteText
                {
                    Font = PiouslyFont.GetFont(size: 15, weight: FontWeight.Bold),
                },
                ChannelFlow = new FillFlowContainer<ChannelListItem>
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Margin = new MarginPadding { Top = 25 },
                    Spacing = new Vector2(0f, 5f),
                },
            };
        }
    }
}
