using osu.Framework.Graphics;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings
{
    public class SettingsCheckbox : SettingsItem<bool>
    {
        private string labelText;

        protected override Drawable CreateControl() => new PiouslyCheckbox();

        public override string LabelText
        {
            get => labelText;
            set => ((PiouslyCheckbox)Control).LabelText = labelText = value;
        }
    }
}
