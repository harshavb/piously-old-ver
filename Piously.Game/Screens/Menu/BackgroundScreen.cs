using osu.Framework.Allocation;
using osu.Framework.Screens;
using Piously.Game.Screens.Backgrounds;

namespace Piously.Game.Screens.Menu
{
    public class BackgroundScreen : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new Background("background"));
        }
    }
}
