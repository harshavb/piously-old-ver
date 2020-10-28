using System;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Overlays.Dialog;

namespace Piously.Game.Overlays.Chat
{
    public class ExternalLinkDialog : PopupDialog
    {
        public ExternalLinkDialog(string url, Action openExternalLinkAction)
        {
            HeaderText = "Just checking...";
            BodyText = $"You are about to leave osu! and open the following link in a web browser:\n\n{url}";

            Icon = FontAwesome.Solid.ExclamationTriangle;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"Yes. Go for it.",
                    Action = openExternalLinkAction
                },
                new PopupDialogCancelButton
                {
                    Text = @"No! Abort mission!"
                },
            };
        }
    }
}
