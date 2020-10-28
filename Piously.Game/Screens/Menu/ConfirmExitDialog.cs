using System;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Overlays.Dialog;

namespace Piously.Game.Screens.Menu
{
    public class ConfirmExitDialog : PopupDialog
    {
        public ConfirmExitDialog(Action confirm, Action cancel)
        {
            HeaderText = "Are you sure you want to exit?";
            BodyText = "Last chance to back out.";

            Icon = FontAwesome.Solid.ExclamationTriangle;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"Goodbye",
                    Action = confirm
                },
                new PopupDialogCancelButton
                {
                    Text = @"Just a little more",
                    Action = cancel
                },
            };
        }
    }
}
