using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Overlays.Settings
{
    class SettingsFooter : FillFlowContainer
    {
        [BackgroundDependencyLoader]
        private void load(PiouslyGameBase game, PiouslyColor colors)
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;
            Padding = new MarginPadding { Top = 20, Bottom = 30 };

            Children = new Drawable[]
            {
                new PiouslySpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = game.Name,
                    Font = PiouslyFont.GetFont(size: 18, weight: FontWeight.Bold),
                },
                new BuildDisplay(game.Version, DebugUtils.IsDebugBuild)
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                }
            };
        }

        private class BuildDisplay : PiouslyAnimatedButton
        {
            private readonly string version;
            private readonly bool isDebug;

            [Resolved]
            private PiouslyColor colors { get; set; }

            public BuildDisplay(string version, bool isDebug)
            {
                this.version = version;
                this.isDebug = isDebug;

                Content.RelativeSizeAxes = Axes.Y;
                Content.AutoSizeAxes = AutoSizeAxes = Axes.X;
                Height = 20;
            }

            [BackgroundDependencyLoader(true)]
            private void load(ChangelogOverlay changelog)
            {
                if (!isDebug)
                    Action = () => changelog?.ShowBuild(PiouslyGameBase.CLIENT_STREAM_NAME, version);

                Add(new PiouslySpriteText
                {
                    Font = PiouslyFont.GetFont(size: 16),

                    Text = version,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Padding = new MarginPadding(5),
                    Colour = isDebug ? colors.Red : Color4.White,
                });
            }
        }
    }
}
