using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LoadGameContainer : VisibilityContainer
    {
        public bool isVisible = false;

        [BackgroundDependencyLoader]
        private void load()
        {

        }

        protected override void PopIn()
        {
            this.FadeTo(1, 0.5);
            isVisible = true;
        }

        protected override void PopOut()
        {
            this.FadeTo(0, 0.5);
            isVisible = false;
        }
    }
}
