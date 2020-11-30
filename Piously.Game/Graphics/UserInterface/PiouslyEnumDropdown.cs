using System;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyEnumDropdown<T> : PiouslyDropdown<T>
        where T : struct, Enum
    {
        public PiouslyEnumDropdown()
        {
            Items = (T[])Enum.GetValues(typeof(T));
        }
    }
}
