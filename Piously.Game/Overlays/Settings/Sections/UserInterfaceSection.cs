using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Overlays.Settings.Sections.UserInterface;

namespace Piously.Game.Overlays.Settings.Sections
{
    public class UserInterfaceSection : SettingsSection
    {
        public override string Header => "User Interface";

        public UserInterfaceSection()
        {
            Children = new Drawable[]
            {
                new GeneralSettings(),
                //new MainMenuSettings(),
                //new SongSelectSettings()
            };
        }
    }
}
