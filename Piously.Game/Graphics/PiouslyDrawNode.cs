using System;
using System.Runtime.CompilerServices;
using osu.Framework.Graphics.Batches;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Primitives;
using Piously.Game.Graphics.Primitives;
using Piously.Game.Graphics.Textures;
using osuTK;

namespace Piously.Game.Graphics
{
    public class PiouslyDrawNode : DrawNode
    {
        public PiouslyDrawNode(IDrawable source)
            : base(source)
        {
        }

        /// <summary>
        /// Draws a triangle to the screen.
        /// </summary>
        /// <param name="texture">The texture to fill the hexagon with.</param>
        /// <param name="vertexTriangle">The hexagon to draw.</param>
        /// <param name="drawColour">The vertex colour.</param>
        /// <param name="textureRect">The texture rectangle.</param>
        /// <param name="vertexAction">An action that adds vertices to a <see cref="VertexBatch{T}"/>.</param>
        /// <param name="inflationPercentage">The percentage amount that <paramref name="textureRect"/> should be inflated.</param>
        /// <param name="textureCoords">The texture coordinates of the hexagon's vertices (translated from the corresponding quad's rectangle).</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void DrawHexagon(PiouslyTexture texture, Hexagon vertexHexagon, ColourInfo drawColor, RectangleF? textureRect = null, Action<TexturedVertex2D> vertexAction = null,
                                    Vector2? inflationPercentage = null, RectangleF? textureCoords = null)
            => texture.DrawHexagon(vertexHexagon, drawColor, textureRect, vertexAction, inflationPercentage, textureCoords);
    }
}
