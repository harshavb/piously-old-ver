using osu.Framework.Graphics;
using Piously.Game.Graphics;
using Piously.Game.Graphics.UserInterface;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics.Sprites;
using System.Collections.Generic;
using osuTK;
using osu.Framework.Allocation;

namespace Piously.Game.Overlays.Comments.Buttons
{
    public class ShowMoreRepliesButton : LoadingButton
    {
        protected override IEnumerable<Drawable> EffectTargets => new[] { text };

        private PiouslySpriteText text;

        public ShowMoreRepliesButton()
        {
            AutoSizeAxes = Axes.Both;
            LoadingAnimationSize = new Vector2(8);
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider)
        {
            IdleColor = colorProvider.Light2;
            HoverColor = colorProvider.Light1;
        }

        protected override Drawable CreateContent() => new Container
        {
            AutoSizeAxes = Axes.Both,
            Child = text = new PiouslySpriteText
            {
                AlwaysPresent = true,
                Font = PiouslyFont.GetFont(size: 12, weight: FontWeight.SemiBold),
                Text = "show more"
            }
        };

        protected override void OnLoadStarted() => text.FadeOut(200, Easing.OutQuint);

        protected override void OnLoadFinished() => text.FadeIn(200, Easing.OutQuint);
    }
}
