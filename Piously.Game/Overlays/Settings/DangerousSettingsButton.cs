using osu.Framework.Allocation;
using Piously.Game.Graphics;

namespace Piously.Game.Overlays.Settings
{
    class DangerousSettingsButton : SettingsButton
    {
        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            BackgroundColour = colors.Pink;

            Hexagons.ColorDark = colors.PinkDark;
            Hexagons.ColorLight = colors.PinkLight;
        }
    }
}
