using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings.Sections
{
    /// <summary>
    /// A slider intended to show a "size" multiplier number, where 1x is 1.0.
    /// </summary>
    internal class SizeSlider : PiouslySliderBar<float>
    {
        public override string TooltipText => Current.Value.ToString(@"0.##x");
    }
}
