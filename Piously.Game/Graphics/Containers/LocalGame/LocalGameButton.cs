using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LocalGameButton : CircularContainer
    {
        private Action action;
        private bool isMouseDown = false;

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
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f, 1f),
                },
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = new FontUsage("Aller", 36, null, false, false),
                    Text = text,
                    Colour = new Colour4(1f, 2.667f, 1.6f, 1f),
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0.3555f, 0f),
                }
            };

            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = colours.Gray1,
                Radius = 10,
                Roundness = 0.6f,
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            if(!isMouseDown)
                this.ResizeTo(new Vector2(3.2f, 0.15f), 50);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if(!isMouseDown)
                this.ResizeTo(new Vector2(3f, 0.15f), 50);
            
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
