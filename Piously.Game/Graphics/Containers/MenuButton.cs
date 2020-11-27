using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using Piously.Game.Screens.Menu;
using osuTK;
using Piously.Game.Graphics.Sprites;

namespace Piously.Game.Graphics.Containers
{
    public class MenuButton : Container
    {
        public SpriteText Label;
        public MenuLogo parentLogo;
        public MenuButtonSprite menuButtonSprite { get; private set; }
        public Action clickAction;
       
        [BackgroundDependencyLoader]
        private void load()
        {
            //Masking = true;
            //BorderThickness = 5;
            //BorderColour = Colour4.Magenta;
            Children = new Drawable[]
            {
                menuButtonSprite = new MenuButtonSprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1f),
                    parentLogo = parentLogo,
                    clickAction = clickAction,
                }
            };
        }

        
    }
}
