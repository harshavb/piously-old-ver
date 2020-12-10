using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LocalGameButton : CircularContainer
    {
        public LocalGameButton(Color4 colour, string text)
        {

            Size = new Vector2(800, 200);
            Masking = true;
            Colour = colour;

            Child = new SpriteText
            {
                RelativeSizeAxes = Axes.Both,
                RelativePositionAxes = Axes.Both,
                Position = new Vector2(0f, 0.4f),
                Font = new FontUsage("Aller", 48, null, false, false),
                Text = text,
            };
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colours)
        {
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
            this.ResizeTo(new Vector2(1200, 200), 200, Easing.OutQuint);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ResizeTo(new Vector2(800, 200), 200, Easing.OutQuint);
            base.OnHoverLost(e);
        }
    }
}
