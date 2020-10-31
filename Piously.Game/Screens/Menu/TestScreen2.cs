using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using Piously.Game.Input.Bindings;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class TestScreen2 : Screen, IKeyBindingHandler<GlobalAction>
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

        public bool OnPressed(GlobalAction action)
        {
            if (action == GlobalAction.TestAction2)
            {
                if (loadComplete) piously.Colour = Color4.LimeGreen;
                return true;
            }
            return false;
        }

        public void OnReleased(GlobalAction action)
        {
            if (action == GlobalAction.TestAction2)
                if (loadComplete) piously.Colour = Color4.White;
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
