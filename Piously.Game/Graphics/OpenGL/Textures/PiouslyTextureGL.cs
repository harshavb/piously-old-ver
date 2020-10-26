using System;
using osu.Framework.Graphics.Batches;
using osu.Framework.Graphics.Primitives;
using osuTK;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.OpenGL.Textures;
using Piously.Game.Graphics.Primitives;

namespace Piously.Game.Graphics.OpenGL.Textures
{
    public abstract class PiouslyTextureGL : TextureGL
    {
        protected PiouslyTextureGL(WrapMode wrapModeS = WrapMode.None, WrapMode wrapModeT = WrapMode.None)
            : base(wrapModeS, wrapModeT)
        {
        }

        /// <summary>
        /// Draws a hexagon to the screen.
        /// </summary>
        /// <param name="vertexHexagon">The hexagon to draw.</param>
        /// <param name="drawColour">The vertex colour.</param>
        /// <param name="textureRect">The texture rectangle.</param>
        /// <param name="vertexAction">An action that adds vertices to a <see cref="VertexBatch{T}"/>.</param>
        /// <param name="inflationPercentage">The percentage amount that <paramref name="textureRect"/> should be inflated.</param>
        /// <param name="textureCoords">The texture coordinates of the hexagon's vertices (translated from the corresponding quad's rectangle).</param>
        internal abstract void DrawHexagon(Hexagon vertexHexagon, ColourInfo drawColor, RectangleF? textureRect = null, Action<TexturedVertex2D> vertexAction = null,
                                            Vector2? inflationPercentage = null, RectangleF? textureCoords = null);
    }
}
