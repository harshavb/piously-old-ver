using System.Collections.Generic;
using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.OpenGL.Buffers;
using osu.Framework.Graphics.OpenGL;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics;
using osuTK.Graphics.ES30;
using osuTK.Graphics;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class HexagonalContainer : HexagonalContainer<Drawable> { }

    public class HexagonalContainer<T> : BufferedContainer<T>
        where T : Drawable
    {
        private static readonly float sin_pi_over_3 = 0.8660254037844386f; 
        private static readonly float tan_pi_over_3 = 1.732050807568877f;

        public static readonly float HEXAGON_INRADIUS = sin_pi_over_3;

        public IShader Shader { get; private set; }

        private readonly HexagonalContainerDrawNodeSharedData sharedData;

        public HexagonalContainer(RenderbufferInternalFormat[] formats = null, bool pixelSnapping = false) =>
            sharedData = new HexagonalContainerDrawNodeSharedData(formats, pixelSnapping);

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders) => Shader = shaders.Load(VertexShaderDescriptor.TEXTURE_2, "TextureHexagon");

        protected override DrawNode CreateDrawNode() => new HexagonalContainerDrawNode(this, sharedData);

        // equation 1 is the line on top
        private float equ1(Vector2 coord)
        {
            return coord.Y - sin_pi_over_3;
        }

        // equation 2 is the top right line
        private float equ2(Vector2 coord)
        {
            return (float)(tan_pi_over_3 * coord.X + coord.Y - 2.0 * sin_pi_over_3);
        }

        // equation 3 is the top left line
        private float equ3(Vector2 coord)
        {
            return (float)(-tan_pi_over_3 * coord.X + coord.Y - 2.0 * sin_pi_over_3);
        }

        // equation 4 is the line on bottom
        private float equ4(Vector2 coord)
        {
            return -coord.Y - sin_pi_over_3;
        }

        // equation 5 is the bottom left line
        private float equ5(Vector2 coord)
        {
            return (float)(tan_pi_over_3 * coord.X - coord.Y - 2.0 * sin_pi_over_3);
        }

        // equation 6 is the bottom right line
        private float equ6(Vector2 coord)
        {
            return (float)(-tan_pi_over_3 * coord.X - coord.Y - 2.0 * sin_pi_over_3);
        }

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos)
        {
            Vector2 norm = screenSpacePos - ScreenSpaceDrawQuad.TopLeft;
            norm = Vector2.Divide(norm, ScreenSpaceDrawQuad.Size);
            norm = (norm - new Vector2(0.5f)) * 2;

            float y1 = equ1(norm);
            float y2 = equ2(norm);
            float y3 = equ3(norm);
            float y4 = equ4(norm);
            float y5 = equ5(norm);
            float y6 = equ6(norm);

            return y1 <= 0.0 && y2 <= 0.0 && y3 <= 0.0 && y4 <= 0.0 && y5 <= 0.0 && y6 <= 0.0;
        }

        private class HexagonalContainerDrawNode : BufferedDrawNode, ICompositeDrawNode
        {
            protected new HexagonalContainer<T> Source => (HexagonalContainer<T>)base.Source;

            protected new CompositeDrawableDrawNode Child => (CompositeDrawableDrawNode)base.Child;

            private IShader hexagonShader;
            private bool drawOriginal;
            private ColourInfo effectColour;
            private BlendingParameters effectBlending;
            private EffectPlacement effectPlacement;

            public HexagonalContainerDrawNode(BufferedContainer<T> source, HexagonalContainerDrawNodeSharedData sharedData)
                : base(source, new CompositeDrawableDrawNode(source), sharedData) { }

            public override void ApplyState()
            {
                base.ApplyState();

                hexagonShader = Source.Shader;

                effectColour = Source.EffectColour;
                effectBlending = Source.DrawEffectBlending;
                effectPlacement = Source.EffectPlacement;

                drawOriginal = Source.DrawOriginal;
            }

            protected override void PopulateContents()
            {
                base.PopulateContents();

                GLWrapper.PushScissorState(false);

                FrameBuffer current = SharedData.CurrentEffectBuffer;
                FrameBuffer target = SharedData.GetNextEffectBuffer();

                GLWrapper.SetBlend(BlendingParameters.None);

                using (BindFrameBuffer(target))
                {
                    Vector2 rotationAndResolution;
                    try
                    {
                        rotationAndResolution.X = Source.Rotation;
                        rotationAndResolution.Y = Math.Max(Source.ScreenSpaceDrawQuad.Width, Source.ScreenSpaceDrawQuad.Height); // shh
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Bruh: " + e.ToString());
                        return;
                    }
                    hexagonShader.GetUniform<Vector2>("g_RotationAndResolution").UpdateValue(ref rotationAndResolution);
                    hexagonShader.Bind();
                    DrawFrameBuffer(current, new RectangleF(0, 0, current.Texture.Width, current.Texture.Height), ColourInfo.SingleColour(Color4.White));
                    hexagonShader.Unbind();
                }

                GLWrapper.PopScissorState();
            }

            protected override void DrawContents()
            {
                if (drawOriginal && effectPlacement == EffectPlacement.InFront)
                    base.DrawContents();

                GLWrapper.SetBlend(effectBlending);

                ColourInfo finalEffectColour = DrawColourInfo.Colour;
                finalEffectColour.ApplyChild(effectColour);

                DrawFrameBuffer(SharedData.CurrentEffectBuffer, DrawRectangle, finalEffectColour);

                if (drawOriginal && effectPlacement == EffectPlacement.Behind)
                    base.DrawContents();
            }

            public List<DrawNode> Children
            {
                get => Child.Children;
                set => Child.Children = value;
            }

            public bool AddChildDrawNodes => RequiresRedraw;
        }

        private class HexagonalContainerDrawNodeSharedData : BufferedDrawNodeSharedData
        {
            public HexagonalContainerDrawNodeSharedData(RenderbufferInternalFormat[] formats, bool pixelSnapping)
                : base(1, formats, pixelSnapping) { }
        }
    }
}
