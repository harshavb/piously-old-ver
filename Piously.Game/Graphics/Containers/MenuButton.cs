using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Piously.Game.Graphics.Sprites;

namespace Piously.Game.Graphics.Containers
{
    public class MenuButton : Container
    {
        public SpriteText Label;
        public MenuLogo parentLogo;
        public MenuButtonSprite menuButtonSprite { get; private set; }
        //public Action clickAction;
        
        //Possible fix for hover logic?
        //public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => menuButtonSprite.ReceivePositionalInputAt(screenSpacePos);

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                menuButtonSprite = new MenuButtonSprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    parentLogo = parentLogo
                }
            };
        }
    }
}
