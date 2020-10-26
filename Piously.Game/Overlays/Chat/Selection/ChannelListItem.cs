using System;
using System.Collections.Generic;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Online.Chat;
using Piously.Game.Graphics.Containers;

namespace Piously.Game.Overlays.Chat.Selection
{
    public class ChannelListItem : PiouslyClickableContainer, IFilterable
    {
        private const float width_padding = 5;
        private const float channel_width = 150;
        private const float text_size = 15;
        private const float transition_duration = 100;

        public readonly Channel Channel;

        private readonly Bindable<bool> joinedBind = new Bindable<bool>();
        private readonly PiouslySpriteText name;
        private readonly PiouslySpriteText topic;
        private readonly SpriteIcon joinedCheckmark;

        private Color4 joinedColour;
        private Color4 topicColour;
        private Color4 hoverColour;

        public IEnumerable<string> FilterTerms => new[] { Channel.Name, Channel.Topic ?? string.Empty };

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1f : 0f, 100);
        }

        public bool FilteringActive { get; set; }

        public Action<Channel> OnRequestJoin;
        public Action<Channel> OnRequestLeave;

        public ChannelListItem(Channel channel)
        {
            Channel = channel;

            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            Action = () => { (channel.Joined.Value ? OnRequestLeave : OnRequestJoin)?.Invoke(channel); };

            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Children = new[]
                            {
                                joinedCheckmark = new SpriteIcon
                                {
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                    Icon = FontAwesome.Solid.CheckCircle,
                                    Size = new Vector2(text_size),
                                    Shadow = false,
                                    Margin = new MarginPadding { Right = 10f },
                                },
                            },
                        },
                        new Container
                        {
                            Width = channel_width,
                            AutoSizeAxes = Axes.Y,
                            Children = new[]
                            {
                                name = new PiouslySpriteText
                                {
                                    Text = channel.ToString(),
                                    Font = PiouslyFont.GetFont(size: text_size, weight: FontWeight.Bold),
                                    Shadow = false,
                                },
                            },
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            Width = 0.7f,
                            AutoSizeAxes = Axes.Y,
                            Margin = new MarginPadding { Left = width_padding },
                            Children = new[]
                            {
                                topic = new PiouslySpriteText
                                {
                                    Text = channel.Topic,
                                    Font = PiouslyFont.GetFont(size: text_size, weight: FontWeight.SemiBold),
                                    Shadow = false,
                                },
                            },
                        },
                        new FillFlowContainer
                        {
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Margin = new MarginPadding { Left = width_padding },
                            Spacing = new Vector2(3f, 0f),
                            Children = new Drawable[]
                            {
                                new SpriteIcon
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Icon = FontAwesome.Solid.User,
                                    Size = new Vector2(text_size - 2),
                                    Shadow = false,
                                },
                                new PiouslySpriteText
                                {
                                    Text = @"0",
                                    Font = PiouslyFont.GetFont(size: text_size, weight: FontWeight.SemiBold),
                                    Shadow = false,
                                },
                            },
                        },
                    },
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            topicColour = colors.Gray9;
            joinedColour = colors.Blue;
            hoverColour = colors.Yellow;

            joinedBind.ValueChanged += joined => updateColour(joined.NewValue);
            joinedBind.BindTo(Channel.Joined);

            joinedBind.TriggerChange();
            FinishTransforms(true);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!Channel.Joined.Value)
                name.FadeColour(hoverColour, 50, Easing.OutQuint);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!Channel.Joined.Value)
                name.FadeColour(Color4.White, transition_duration);
        }

        private void updateColour(bool joined)
        {
            if (joined)
            {
                name.FadeColour(Color4.White, transition_duration);
                joinedCheckmark.FadeTo(1f, transition_duration);
                topic.FadeTo(0.8f, transition_duration);
                topic.FadeColour(Color4.White, transition_duration);
                this.FadeColour(joinedColour, transition_duration);
            }
            else
            {
                joinedCheckmark.FadeTo(0f, transition_duration);
                topic.FadeTo(1f, transition_duration);
                topic.FadeColour(topicColour, transition_duration);
                this.FadeColour(Color4.White, transition_duration);
            }
        }
    }
}
