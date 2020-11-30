using System;
using osu.Framework.Graphics.UserInterface;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyMenuItem : MenuItem
    {
        public readonly MenuItemType Type;

        public PiouslyMenuItem(string text, MenuItemType type = MenuItemType.Standard)
            : this(text, type, null)
        {
        }

        public PiouslyMenuItem(string text, MenuItemType type, Action action)
            : base(text, action)
        {
            Type = type;
        }
    }
}
