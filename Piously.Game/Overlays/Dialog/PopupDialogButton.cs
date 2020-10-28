using osu.Framework.Extensions.Color4Extensions;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Dialog
{
    public class PopupDialogButton : DialogButton
    {
        public PopupDialogButton()
        {
            Height = 50;
            BackgroundColour = Color4Extensions.FromHex(@"150e14");
            TextSize = 18;
        }
    }
}
