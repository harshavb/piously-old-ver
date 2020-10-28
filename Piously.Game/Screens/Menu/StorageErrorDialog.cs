using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using Piously.Game.IO;
using Piously.Game.Overlays;
using Piously.Game.Overlays.Dialog;

namespace Piously.Game.Screens.Menu
{
    public class StorageErrorDialog : PopupDialog
    {
        [Resolved]
        private DialogOverlay dialogOverlay { get; set; }

        [Resolved]
        private PiouslyGameBase piouslyGame { get; set; }

        public StorageErrorDialog(PiouslyStorage storage, PiouslyStorageError error)
        {
            HeaderText = "Piously storage error";
            Icon = FontAwesome.Solid.ExclamationTriangle;

            var buttons = new List<PopupDialogButton>();

            switch (error)
            {
                case PiouslyStorageError.NotAccessible:
                    BodyText = $"The specified Piously data location (\"{storage.CustomStoragePath}\") is not accessible. If it is on external storage, please reconnect the device and try again.";

                    buttons.AddRange(new PopupDialogButton[]
                    {
                        new PopupDialogCancelButton
                        {
                            Text = "Try again",
                            Action = () =>
                            {
                                if (!storage.TryChangeToCustomStorage(out var nextError))
                                    dialogOverlay.Push(new StorageErrorDialog(storage, nextError));
                            }
                        },
                        new PopupDialogCancelButton
                        {
                            Text = "Use default location until restart",
                        },
                        new PopupDialogOkButton
                        {
                            Text = "Reset to default location",
                            Action = storage.ResetCustomStoragePath
                        },
                    });
                    break;

                case PiouslyStorageError.AccessibleButEmpty:
                    BodyText = $"The specified Piously data location (\"{storage.CustomStoragePath}\") is empty. If you have moved the files, please close osu! and move them back.";

                    // Todo: Provide the option to search for the files similar to migration.
                    buttons.AddRange(new PopupDialogButton[]
                    {
                        new PopupDialogCancelButton
                        {
                            Text = "Start fresh at specified location"
                        },
                        new PopupDialogOkButton
                        {
                            Text = "Reset to default location",
                            Action = storage.ResetCustomStoragePath
                        },
                    });

                    break;
            }

            Buttons = buttons;
        }
    }
}
