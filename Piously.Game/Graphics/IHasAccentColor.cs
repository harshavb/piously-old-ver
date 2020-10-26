using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Transforms;

namespace Piously.Game.Graphics
{
    public interface IHasAccentColor : IDrawable
    {
        Color4 AccentColor { get; set; }
    }

    public static class AccentedColourExtensions
    {
        /// <summary>
        /// Smoothly adjusts <see cref="IHasAccentColour.AccentColour"/> over time.
        /// </summary>
        /// <returns>A <see cref="TransformSequence{T}"/> to which further transforms can be added.</returns>
        public static TransformSequence<T> FadeAccent<T>(this T accentedDrawable, Color4 newColor, double duration = 0, Easing easing = Easing.None)
            where T : class, IHasAccentColor
            => accentedDrawable.TransformTo(nameof(accentedDrawable.AccentColor), newColor, duration, easing);

        /// <summary>
        /// Smoothly adjusts <see cref="IHasAccentColour.AccentColour"/> over time.
        /// </summary>
        /// <returns>A <see cref="TransformSequence{T}"/> to which further transforms can be added.</returns>
        public static TransformSequence<T> FadeAccent<T>(this TransformSequence<T> t, Color4 newColor, double duration = 0, Easing easing = Easing.None)
            where T : Drawable, IHasAccentColor
            => t.Append(o => o.FadeAccent(newColor, duration, easing));
    }
}
