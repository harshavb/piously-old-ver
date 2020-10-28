using osu.Framework.Allocation;
using Piously.Game.Graphics;
namespace Piously.Game.Overlays.Dialog
{
    public class PopupDialogOkButton : PopupDialogButton
    {
        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            ButtonColour = colors.Pink;
        }
    }
}
