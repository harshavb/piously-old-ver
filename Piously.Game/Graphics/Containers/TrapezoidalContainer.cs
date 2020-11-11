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
    public class TrapezoidalContainer : TrapezoidalContainer<Drawable> { }

    public class TrapezoidalContainer<T> : BufferedContainer<T>
        where T : Drawable
    {
        private static readonly float sin_pi_over_3 = (float)Math.Sin(Math.PI / 3);
        private static readonly float tan_pi_over_3 = (float)Math.Tan(Math.PI / 3);

        public static readonly float TRAPEZOID_INRADIUS = sin_pi_over_3;

        public IShader Shader { get; private set; }

        private readonly TrapezoidalContainerDrawNodeSharedData sharedData;

        public TrapezoidalContainer(RenderbufferInternalFormat[] formats = null, bool pixelSnapping = false) =>
            this.sharedData = new TrapezoidalContainerDrawNodeSharedData(formats, pixelSnapping);

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders) => this.Shader = shaders.Load(VertexShaderDescriptor.TEXTURE_2, "TextureTrapezoid");

        protected override DrawNode CreateDrawNode() => new TrapezoidalContainerDrawNode(this, sharedData);

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos)
        {
            Vector2 norm = screenSpacePos - this.ScreenSpaceDrawQuad.TopLeft;
            norm = new Vector2(norm.X / this.ScreenSpaceDrawQuad.Width, norm.Y / this.ScreenSpaceDrawQuad.Height); // apparently we can't divide Vector2s?
            norm = (norm - new Vector2(0.5f, 0.0f)) * new Vector2(2.0f, 0.0f);

            // trapezoids are horizontally symmetrical, but not vertically, so we only test a bottom and top quadrant :V
            norm = new Vector2(Math.Abs(norm.X), norm.Y);

            if (norm.Y > sin_pi_over_3)
            {
                return false; // top bound
            }

            if (norm.Y > -tan_pi_over_3 * (norm.X - 1))
            {
                return false; // right bound
            }

            if(norm.Y < 0)
            {
                return false; // bottom bound
            }

            return true;
        }

        private class TrapezoidalContainerDrawNode : BufferedDrawNode, ICompositeDrawNode
        {
            protected new TrapezoidalContainer<T> Source => (TrapezoidalContainer<T>)base.Source;

            protected new CompositeDrawableDrawNode Child => (CompositeDrawableDrawNode)base.Child;

            private IShader trapezoidShader;
            private bool drawOriginal;
            private ColourInfo effectColour;
            private BlendingParameters effectBlending;
            private EffectPlacement effectPlacement;

            public TrapezoidalContainerDrawNode(BufferedContainer<T> source, TrapezoidalContainerDrawNodeSharedData sharedData)
                : base(source, new CompositeDrawableDrawNode(source), sharedData) { }

            public override void ApplyState()
            {
                base.ApplyState();

                trapezoidShader = Source.Shader;

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
                    trapezoidShader.Bind();
                    DrawFrameBuffer(current, new RectangleF(0, 0, current.Texture.Width, current.Texture.Height), ColourInfo.SingleColour(Color4.White));
                    trapezoidShader.Unbind();
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

        private class TrapezoidalContainerDrawNodeSharedData : BufferedDrawNodeSharedData
        {
            public TrapezoidalContainerDrawNodeSharedData(RenderbufferInternalFormat[] formats, bool pixelSnapping)
                : base(2, formats, pixelSnapping) { }
        }
    }
}
