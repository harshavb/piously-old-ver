using System;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Textures;
using osuTK;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.OpenGL;
using osu.Framework.Graphics;

namespace Piously.Game.Graphics.Shapes
{
    /// <summary>
    /// A simple trapezoid. Can be colored using the <see cref="Drawable.Colour"/> property.
    /// </summary>
    public class Trapezoid : Sprite
    {
        public Trapezoid()
        {
            Texture = Texture.WhitePixel;
        }

        public override RectangleF BoundingBox => toTrapezoid(ToParentSpace(LayoutRectangle)).AABBFloat;

        private static Primitives.Trapezoid toTrapezoid(Quad q) => new Primitives.Trapezoid(
            (q.TopLeft + q.BottomLeft) / 2,
            (q.TopRight + q.BottomRight) / 2);

        public override bool Contains(Vector2 screenSpacePos) => toTrapezoid(ScreenSpaceDrawQuad).Contains(screenSpacePos);

        protected override DrawNode CreateDrawNode() => new TrapezoidDrawNode(this);

        private class TrapezoidDrawNode : SpriteDrawNode
        {
            public TrapezoidDrawNode(Trapezoid source) : base(source)
            {

            }

            protected override void Blit(Action<TexturedVertex2D> vertexAction)
            {
                Primitives.Trapezoid drawingTrap = toTrapezoid(ScreenSpaceDrawQuad);
                DrawTriangle(Texture, drawingTrap.upTriangle, DrawColourInfo.Colour, null, null,
                    new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height), TextureCoords);
                DrawTriangle(Texture, drawingTrap.downTriangle, DrawColourInfo.Colour, null, null,
                    new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height), TextureCoords);

            }

            protected override void BlitOpaqueInterior(Action<TexturedVertex2D> vertexAction)
            {
                Primitives.Trapezoid drawingTrap = toTrapezoid(ScreenSpaceDrawQuad);

                if (GLWrapper.IsMaskingActive)
                {
                    DrawClipped(ref drawingTrap, Texture, DrawColourInfo.Colour, vertexAction: vertexAction);
                }
                else
                {
                    DrawTriangle(Texture, drawingTrap.upTriangle, DrawColourInfo.Colour, null, null,
                        new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height), TextureCoords);
                    DrawTriangle(Texture, drawingTrap.downTriangle, DrawColourInfo.Colour, null, null,
                        new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height), TextureCoords);
                }
            }
        }
    }
}
