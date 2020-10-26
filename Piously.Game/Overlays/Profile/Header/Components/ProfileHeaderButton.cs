using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics.Containers;

namespace Piously.Game.Overlays.Profile.Header.Components
{
    public abstract class ProfileHeaderButton : PiouslyHoverContainer
    {
        private readonly Box background;
        private readonly Container content;

        protected override Container<Drawable> Content => content;

        protected override IEnumerable<Drawable> EffectTargets => new[] { background };

        protected ProfileHeaderButton()
        {
            AutoSizeAxes = Axes.X;

            base.Content.Add(new CircularContainer
            {
                Masking = true,
                AutoSizeAxes = Axes.X,
                RelativeSizeAxes = Axes.Y,
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    content = new Container
                    {
                        AutoSizeAxes = Axes.X,
                        RelativeSizeAxes = Axes.Y,
                        Padding = new MarginPadding { Horizontal = 10 },
                    }
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider)
        {
            IdleColor = colorProvider.Background6;
            HoverColor = colorProvider.Background5;
        }
    }
}
