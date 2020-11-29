using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Overlays.Settings
{
    public class SettingsFooter : FillFlowContainer
    {
        [BackgroundDependencyLoader]
        private void load(PiouslyGameBase game, PiouslyColour colours)
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;
            Padding = new MarginPadding { Top = 20, Bottom = 30 };

            var modes = new List<Drawable>();

            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Direction = FillDirection.Full,
                    AutoSizeAxes = Axes.Both,
                    Children = modes,
                    Spacing = new Vector2(5),
                    Padding = new MarginPadding { Bottom = 10 },
                },
                new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = game.Name,
                    Font = new FontUsage(size: 18)
                }
            };
        }
    }
}
