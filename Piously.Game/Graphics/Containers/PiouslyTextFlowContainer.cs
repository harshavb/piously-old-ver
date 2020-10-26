using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.Sprites;

namespace Piously.Game.Graphics.Containers
{
    public class PiouslyTextFlowContainer : TextFlowContainer
    {
        public PiouslyTextFlowContainer(Action<SpriteText> defaultCreationParameters = null)
            : base(defaultCreationParameters)
        {
        }

        protected override SpriteText CreateSpriteText() => new PiouslySpriteText();

        public void AddArbitraryDrawable(Drawable drawable) => AddInternal(drawable);

        public IEnumerable<Drawable> AddIcon(IconUsage icon, Action<SpriteText> creationParameters = null) => AddText(icon.Icon.ToString(), creationParameters);
    }
}
