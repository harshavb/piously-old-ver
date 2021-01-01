using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using Piously.Game.Graphics.Backgrounds;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LocalGameButton : CircularContainer
    {
        private Action action;
        protected Box Hover;
        private bool isMouseDown = false;
        public bool IsCreateGame = false;

        private string text = "";

        public Action Action
        {
            get => action;
            set
            {
                if (action == value) return;

                action = value;
            }
        }

        public string Text
        {
            get => text;
            set
            {
                if (text == value) return;

                text = value;
            }
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colours)
        {
            Size = new Vector2(3f, 0.15f);
            Masking = true;

            Children = new Drawable[]
            {
                Hover = new Box
                {
                        Alpha = 0,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.White.Opacity(.1f),
                        Blending = BlendingParameters.Additive,
                        Depth = float.MinValue
                },
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours.Pink,
                    Size = new Vector2(1f, 1f),
                },
                new Hexagons
                {
                    HexagonScale = 5,
                    ColourLight = colours.PinkLight,
                    ColourDark = colours.PinkDark,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f, 3f),
                },
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = new FontUsage("Aller", 36, null, false, false),
                    Text = text,
                    Colour = Color4.White,
                    RelativePositionAxes = Axes.Both,
                    Position = IsCreateGame ? new Vector2(0.3935f, 0f) : new Vector2(0.3955f, 0f),
                },
            };

            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = colours.Gray1,
                Radius = 10,
                Roundness = 1f,
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            Hover.FadeIn(200, Easing.OutQuint);

            if (!isMouseDown)
                this.ResizeTo(new Vector2(3.2f, 0.15f), 400, Easing.OutElasticHalf);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (!isMouseDown)
                this.ResizeTo(new Vector2(3f, 0.15f), 400, Easing.OutElasticHalf);
        }

        protected override bool OnClick(ClickEvent e)
        {

            trigger();
            return true;
        }

        private void trigger()
        {
            action?.Invoke();
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.ResizeTo(new Vector2(3.25f, 0.15f), 50);
            isMouseDown = true;
            return false;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if(IsHovered)
                this.ResizeTo(new Vector2(3.2f, 0.15f), 50);
            else
                this.ResizeTo(new Vector2(3f, 0.15f), 50);
            isMouseDown = false;
        }
    }
}
