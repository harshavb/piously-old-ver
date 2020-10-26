using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.UserInterface;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyContextMenu : PiouslyMenu
    {
        private const int fade_duration = 250;

        public PiouslyContextMenu()
            : base(Direction.Vertical)
        {
            MaskingContainer.CornerRadius = 5;
            MaskingContainer.EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(0.1f),
                Radius = 4,
            };

            ItemsContainer.Padding = new MarginPadding { Vertical = DrawablePiouslyMenuItem.MARGIN_VERTICAL };

            MaxHeight = 250;
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            BackgroundColour = colors.ContextMenuGray;
        }

        protected override void AnimateOpen() => this.FadeIn(fade_duration, Easing.OutQuint);
        protected override void AnimateClose() => this.FadeOut(fade_duration, Easing.OutQuint);

        protected override Menu CreateSubMenu() => new PiouslyContextMenu();
    }
}
