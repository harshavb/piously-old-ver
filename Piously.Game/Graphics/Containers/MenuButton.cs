using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using Piously.Game.Graphics.Sprites;

namespace Piously.Game.Graphics.Containers
{
    public class MenuButton : Container
    {
        public Action clickAction;
        public SpriteText Label;
        public MenuLogo parentLogo;
        public MenuButtonSprite menuButtonSprite { get; private set; }
        
        //Possible fix for hover logic?
        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => menuButtonSprite.ReceivePositionalInputAt(screenSpacePos);

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

        protected override bool OnClick(ClickEvent e)
        {
            trigger();
            return true;
        }

        private void trigger()
        {
            clickAction?.Invoke();
        }
    }
}
