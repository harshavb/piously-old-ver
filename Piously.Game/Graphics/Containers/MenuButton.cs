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
        public MenuButtonSprite menuButtonSprite { get; private set; }
        //public Action clickAction;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                menuButtonSprite = new MenuButtonSprite
                {
                    RelativeSizeAxes = Axes.Both,
                    parentLogo = parentLogo
                }
            };
        }

        public void ScaleTo(float newScale, double duration = 0, Easing Easing = default)
        {
            menuButtonSprite.ScaleTo(newScale, duration, Easing);
        }
    }
}
