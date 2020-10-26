using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Piously.Game.Users.Drawables
{
    public class DrawableFlag : Sprite, IHasTooltip
    {
        private readonly Country country;

        public string TooltipText => country?.FullName;

        public DrawableFlag(Country country)
        {
            this.country = country;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore ts)
        {
            if (ts == null)
                throw new ArgumentNullException(nameof(ts));

            Texture = ts.Get($@"Flags/{country?.FlagName ?? @"__"}") ?? ts.Get(@"Flags/__");
        }
    }
}
