using osu.Framework.Allocation;
using Piously.Game.Graphics;
using Piously.Game.Online.Chat;

namespace Piously.Game.Overlays.Chat.Tabs
{
    public class ChannelSelectorTabItem : ChannelTabItem
    {
        public override bool IsRemovable => false;

        public override bool IsSwitchable => false;

        protected override bool IsBoldWhenActive => false;

        public ChannelSelectorTabItem()
            : base(new ChannelSelectorTabChannel())
        {
            Depth = float.MaxValue;
            Width = 45;

            Icon.Alpha = 0;

            Text.Font = Text.Font.With(size: 45);
            Text.Truncate = false;
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor color)
        {
            BackgroundInactive = color.Gray2;
            BackgroundActive = color.Gray3;
        }

        public class ChannelSelectorTabChannel : Channel
        {
            public ChannelSelectorTabChannel()
            {
                Name = "+";
                Type = ChannelType.System;
            }
        }
    }
}
