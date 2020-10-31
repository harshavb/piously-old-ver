using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using Piously.Game.Graphics.Containers;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class TestScreen2 : Screen
    {
        private bool loadComplete = false;

        SpriteText piously;
        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(piously = new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = "piously moments",
                Font = new FontUsage(family: "InkFree", size: 200f)
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            loadComplete = true;
        }

        public void rotateText()
        {
            if(loadComplete) piously.Rotation += (float)Time.Elapsed / 2;
        }
    }
}
