using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Overlays.Settings.Sections.Input;

namespace Piously.Game.Overlays.Settings.Sections
{
    public class InputSection : SettingsSection
    {
        public override string Header => "Input";

        public InputSection()
        {
            Children = new Drawable[]
            {
                new MouseSettings(),
                //TO BE IMPLEMENTED after keybindingstores
                //new KeyboardSettings(keyConfig),
            };
        }
    }
}
