using System;
using System.IO;
using osu.Framework.Graphics.Batches;
using osu.Framework.Graphics.OpenGL.Textures;
using osu.Framework.Graphics.Primitives;
using osuTK;
using osuTK.Graphics.ES30;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Textures;
using RectangleF = osu.Framework.Graphics.Primitives.RectangleF;
using Piously.Game.Graphics.Primitives;
using Piously.Game.Graphics.OpenGL.Textures;

namespace Piously.Game.Graphics.Textures
{
    public class PiouslyTexture : Texture
    {
        public virtual PiouslyTextureGL PiouslyTextureGL { get; }

        public PiouslyTexture(int width, int height, bool manualMipmaps = false, All filteringMode = All.Linear)
            : base(width, height, manualMipmaps, filteringMode)
        {
        }

        public PiouslyTexture(TextureGL textureGl)
            : base(textureGl)
        {
            PiouslyTextureGL = (PiouslyTextureGL) textureGl;
        }

        internal void DrawHexagon(Hexagon vertexHexagon, ColourInfo drawColor, RectangleF? textureRect = null, Action<TexturedVertex2D> vertexAction = null,
                                  Vector2? inflationPercentage = null, RectangleF? textureCoords = null)
        {
            if (PiouslyTextureGL == null || !PiouslyTextureGL.Bind()) return;

            PiouslyTextureGL.DrawHexagon(vertexHexagon, drawColor, TextureBounds(textureRect), vertexAction, inflationPercentage, TextureBounds(textureCoords));
        }
    }
}
