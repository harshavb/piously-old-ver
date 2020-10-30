using System;
using osu.Framework.Graphics;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings
{
    public class SettingsEnumDropdown<T> : SettingsDropdown<T>
        where T : struct, Enum
    {
        protected override PiouslyDropdown<T> CreateDropdown() => new DropdownControl();

        protected new class DropdownControl : PiouslyEnumDropdown<T>
        {
            public DropdownControl()
            {
                Margin = new MarginPadding { Top = 5 };
                RelativeSizeAxes = Axes.X;
            }
        }
    }
}
