using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK.Graphics;
using System.Collections.Generic;

namespace Piously.Game.Graphics.Containers
{
    public class PiouslyHoverContainer : PiouslyClickableContainer
    {
        protected const float FADE_DURATION = 500;

        protected Color4 HoverColor;

        protected Color4 IdleColor = Color4.White;

        protected virtual IEnumerable<Drawable> EffectTargets => new[] { Content };

        public PiouslyHoverContainer()
        {
            Enabled.ValueChanged += e =>
            {
                if (isHovered)
                {
                    if (e.NewValue)
                        fadeIn();
                    else
                        fadeOut();
                }
            };
        }

        private bool isHovered;

        protected override bool OnHover(HoverEvent e)
        {
            if (isHovered)
                return false;

            isHovered = true;

            if (!Enabled.Value)
                return false;

            fadeIn();

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!isHovered)
                return;

            isHovered = false;
            fadeOut();

            base.OnHoverLost(e);
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            if (HoverColor == default)
                HoverColor = colors.Yellow;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            EffectTargets.ForEach(d => d.FadeColour(IdleColor));
        }

        private void fadeIn() => EffectTargets.ForEach(d => d.FadeColour(HoverColor, FADE_DURATION, Easing.OutQuint));

        private void fadeOut() => EffectTargets.ForEach(d => d.FadeColour(IdleColor, FADE_DURATION, Easing.OutQuint));
    }
}
