using osu.Framework.Allocation;
using Piously.Game.Graphics;

namespace Piously.Game.Overlays.Dialog
{
    public class PopupDialogCancelButton : PopupDialogButton
    {
        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            ButtonColour = colors.Blue;
        }
    }
}
