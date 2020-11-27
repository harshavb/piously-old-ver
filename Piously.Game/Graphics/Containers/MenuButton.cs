using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.Sprites;

namespace Piously.Game.Graphics.Containers
{
    public class MenuButton : Container
    {
        public SpriteText Label;
        public MenuLogo parentLogo;
        //public Action clickAction;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new MenuButtonSprite
                {
                    RelativeSizeAxes = Axes.Both,
                    parentLogo = parentLogo
                }
            };
        }
    }
}
