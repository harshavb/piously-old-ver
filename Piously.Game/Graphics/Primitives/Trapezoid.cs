using osuTK;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using osu.Framework.Graphics.Primitives;

namespace Piously.Game.Graphics.Primitives
{
    [StructLayout(LayoutKind.Sequential)]
    public class Trapezoid : IConvexPolygon, IEquatable<Trapezoid>
    {
        // Note: Do not change the order of vertices. They are ordered in screen-space counter-clockwise fashion.
        // See: IPolygon.GetVertices()
        public const float SQRT_3 = 1.73205f;
        public readonly Vector2 center;
        public readonly Vector2 P0;
        public readonly Vector2 P1;
        public readonly Vector2 P2;
        public readonly Vector2 P3;

        public readonly Triangle upTriangle;
        public readonly Triangle downTriangle;


        /// <summary>
        /// Creates a regular Trapezoid from the base points extending to the right of p0 -> p3; the center will be (middle_x, middle_y).
        /// </summary>
        /// <param name="p0">The left base point.</param>
        /// <param name="p1">The right base point.</param>
        public Trapezoid(Vector2 p0, Vector2 p3)
        {
            P0 = p0;
            P3 = p3;

            center = (p0 + p3) / 2;

            Vector2 p3ToCenter = p3 - center;
            Vector2 betweenCenterAndP3 = (p3 + center) / 2;
            Vector2 inverseP3CenterVector = p3ToCenter.PerpendicularLeft.Normalized();
            P2 = betweenCenterAndP3 + 0.5f * p3ToCenter.Length * SQRT_3 * inverseP3CenterVector;

            Vector2 p0ToCenter = center - p0;
            Vector2 betweenCenterAndP0 = (center + p0) / 2;
            Vector2 inverseP0CenterVector = p0ToCenter.PerpendicularLeft.Normalized();
            P1 = betweenCenterAndP0 + 0.5f * p0ToCenter.Length * SQRT_3 * inverseP0CenterVector;

            downTriangle = new Triangle(P0, P1, P2);
            upTriangle = new Triangle(P0, P2, P3);

        }

        public ReadOnlySpan<Vector2> GetAxisVertices() => GetVertices();

        public ReadOnlySpan<Vector2> GetVertices() => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in P0), 4);

        public bool Equals(Trapezoid other) =>
            P0 == other.P0 &&
            P3 == other.P3;

        /// <summary>
        /// Checks whether a point lies within the Trapezoid.
        /// </summary>
        /// <param name="pos">The point to check.</param>
        /// <returns>Outcome of the check.</returns>
        public bool Contains(Vector2 pos) => upTriangle.Contains(pos) || downTriangle.Contains(pos);

        public RectangleF AABBFloat
        {
            get
            {
                float xMin = Math.Min(P0.X, Math.Min(P1.X, Math.Min(P2.X, P3.X)));
                float yMin = Math.Min(P0.Y, Math.Min(P1.Y, Math.Min(P2.Y, P3.Y)));
                float xMax = Math.Max(P0.X, Math.Max(P1.X, Math.Max(P2.X, P3.X)));
                float yMax = Math.Max(P0.Y, Math.Max(P1.Y, Math.Max(P2.Y, P3.Y)));

                return new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin);
            }
        }

        public float Area => (upTriangle.Area + downTriangle.Area);
    }
}
