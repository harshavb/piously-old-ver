using System;
using osu.Framework.Input.Bindings;

namespace Piously.Game.Input.Bindings
{
    public class PiouslyKeyBinding : KeyBinding, IComparable
    {
        public PiouslyKeyBinding(KeyCombination keyCombination, GlobalAction action)
            : base(keyCombination, action)
        {  
        }

        public int CompareTo(object ob)
        {
            return ((GlobalAction)Action > (GlobalAction)((PiouslyKeyBinding)ob).Action) ? 1 : 
                (((GlobalAction)Action < (GlobalAction)((PiouslyKeyBinding)ob).Action) ? -1 : 0);
        }
    }
}
