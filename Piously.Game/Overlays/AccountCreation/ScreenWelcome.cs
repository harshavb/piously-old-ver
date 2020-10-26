using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Overlays.Settings;
using Piously.Game.Screens.Menu;
using osuTK;

namespace Piously.Game.Overlays.AccountCreation
{
    public class ScreenWelcome : AccountCreationScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Padding = new MarginPadding(20),
                Spacing = new Vector2(0, 5),
                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 150,
                        Child = new PiouslyLogo
                        {
                            Scale = new Vector2(0.1f),
                            Anchor = Anchor.Centre,
                            Hexagons = false,
                        },
                    },
                    new PiouslySpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Font = PiouslyFont.GetFont(size: 24, weight: FontWeight.Light),
                        Text = "New Player Registration",
                    },
                    new PiouslySpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Font = PiouslyFont.GetFont(size: 12),
                        Text = "let's get you started",
                    },
                    new SettingsButton
                    {
                        Text = "Let's create an account!",
                        Margin = new MarginPadding { Vertical = 120 },
                        Action = () => this.Push(new ScreenWarning())
                    }
                }
            };
        }
    }
}
