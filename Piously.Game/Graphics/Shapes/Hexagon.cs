using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Piously.Game.Graphics.Shapes
{
    /// <summary>
    /// A simple hexagon. Can be colored using the <see cref="Drawable.Colour"/> property.
    /// </summary>
    public class Hexagon : Sprite
    {
        public Hexagon()
        {
            base.Texture = Texture.WhitePixel;
        }

        public override Texture Texture
        {
            get => base.Texture;
            set => throw new InvalidOperationException($"The texture of a {nameof(Hexagon)} cannot be set");
        }
    }
}
