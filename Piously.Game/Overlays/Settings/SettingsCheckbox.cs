using osu.Framework.Graphics;
using osu.Framework.Localisation;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings
{
    public class SettingsCheckbox : SettingsItem<bool>
    {
        private LocalisableString labelText;

        protected override Drawable CreateControl() => new PiouslyCheckbox();

        public override LocalisableString LabelText
        {
            get => labelText;
            set => ((PiouslyCheckbox)Control).LabelText = labelText = value;
        }
    }
}
