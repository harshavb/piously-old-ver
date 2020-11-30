using System.Collections.Generic;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings
{
    public class SettingsDropdown<T> : SettingsItem<T>
    {
        protected new PiouslyDropdown<T> Control => (PiouslyDropdown<T>)base.Control;

        public IEnumerable<T> Items
        {
            get => Control.Items;
            set => Control.Items = value;
        }

        public IBindableList<T> ItemSource
        {
            get => Control.ItemSource;
            set => Control.ItemSource = value;
        }

        public override IEnumerable<string> FilterTerms => base.FilterTerms.Concat(Control.Items.Select(i => i.ToString()));

        protected sealed override Drawable CreateControl() => CreateDropdown();

        protected virtual PiouslyDropdown<T> CreateDropdown() => new DropdownControl();

        protected class DropdownControl : PiouslyDropdown<T>
        {
            public DropdownControl()
            {
                Margin = new MarginPadding { Top = 5 };
                RelativeSizeAxes = Axes.X;
            }

            protected override DropdownMenu CreateMenu() => base.CreateMenu().With(m => m.MaxHeight = 200);
        }
    }
}
