using System;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using Piously.Game.Input.Bindings;

namespace Piously.Game.Overlays.Volume
{
    public class VolumeControlReceptor : Container, IScrollBindingHandler<GlobalAction>, IHandleGlobalKeyboardInput
    {
        public Func<GlobalAction, bool> ActionRequested;
        public Func<GlobalAction, float, bool, bool> ScrollActionRequested;

        public bool OnPressed(GlobalAction action) =>
            ActionRequested?.Invoke(action) ?? false;

        public bool OnScroll(GlobalAction action, float amount, bool isPrecise) =>
            ScrollActionRequested?.Invoke(action, amount, isPrecise) ?? false;

        public void OnReleased(GlobalAction action)
        {
        }
    }
}
