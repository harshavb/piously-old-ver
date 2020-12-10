using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LocalGameButton : CircularContainer
    {
        public string Text { get; set; }

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colours)
        {
            Size = new Vector2(0.8f, 0.15f);
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
                    Font = new FontUsage("Aller", 48, null, false, false),
                    Text = Text,
                    Colour = new Colour4(3f, 3f, 3f, 1f),
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
            this.ResizeTo(new Vector2(1f, 0.15f), 50);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ResizeTo(new Vector2(0.8f, 0.15f), 50);
            base.OnHoverLost(e);
        }
    }
}
