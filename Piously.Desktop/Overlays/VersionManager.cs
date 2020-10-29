using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using Piously.Game;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace Piously.Desktop.Overlays
{
    public class VersionManager : VisibilityContainer
    {
        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors, TextureStore textures, PiouslyGameBase game)
        {
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.BottomCentre;
            Origin = Anchor.BottomCentre;

            Alpha = 0;

            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(5),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Children = new Drawable[]
                            {
                                new PiouslySpriteText
                                {
                                    Font = PiouslyFont.GetFont(weight: FontWeight.Bold),
                                    Text = game.Name
                                },
                                new PiouslySpriteText
                                {
                                    Colour = DebugUtils.IsDebugBuild ? colors.Red : Color4.White,
                                    Text = game.Version
                                },
                            }
                        },
                        new PiouslySpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Font = PiouslyFont.Numeric.With(size: 12),
                            Colour = colors.Yellow,
                            Text = @"Development Build"
                        },
                        new Sprite
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Texture = textures.Get(@"Menu/dev-build-footer"),
                        },
                    }
                }
            };
        }

        protected override void PopIn()
        {
            this.FadeIn(1400, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this.FadeOut(500, Easing.OutQuint);
        }
    }
}
