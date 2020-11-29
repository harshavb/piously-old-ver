using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Graphics.Overlays
{
    public class SettingsOverlay : OverlayContainer
    {
        private Container<Drawable> ContentContainer;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = ContentContainer = new Container
            {
                Width = 400,
                Height = 800,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        Scale = new Vector2(2, 1), // over-extend to the left for transitions
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black,
                        Alpha = 0.6f,
                    }
                }
            };
        }

        protected override void PopIn()
        {
            ContentContainer.MoveToX(ExpandedPosition, 600, Easing.OutQuint);

            this.FadeTo(1, 600, Easing.OutQuint);
        }

        protected virtual float ExpandedPosition => 0;

        protected override void PopOut()
        {
            ContentContainer.MoveToX(-400, 600, Easing.OutQuint);

            this.FadeTo(0, 600, Easing.OutQuint);
        }
    }
}
