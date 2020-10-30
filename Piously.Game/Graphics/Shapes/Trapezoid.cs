using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Piously.Game.Graphics.Shapes
{
    /// <summary>
    /// A simple trapezoid. Can be colored using the <see cref="Drawable.Colour"/> property.
    /// </summary>
    public class Trapezoid : Sprite
    {
        public Trapezoid()
        {
            base.Texture = Texture.WhitePixel;
        }

        public override Texture Texture
        {
            get => base.Texture;
            set => throw new InvalidOperationException($"The texture of a {nameof(Trapezoid)} cannot be set");
        }
    }
}
