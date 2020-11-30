using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Overlays.Settings.Sections.Debug;

namespace Piously.Game.Overlays.Settings.Sections
{
    public class DebugSection : SettingsSection
    {
        public override string Header => "Debug";

        public DebugSection()
        {
            Children = new Drawable[]
            {
                new GeneralSettings(),
                new MemorySettings(),
            };
        }
    }
}
