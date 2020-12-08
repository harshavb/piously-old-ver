using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LeftPanelContainer : Container
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(0.4f, 1f);
            Position = new Vector2(0.025f, 0f);
            Children = new Drawable[] {

                // CreateGame
                new SpriteText
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0f, 0.2f),
                    Font = new FontUsage("Aller", 48, null, false, false),
                    Text = "Create Game",
                },

                // LoadGame
                new SpriteText
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0f, 0.4f),
                    Font = new FontUsage("Aller", 48, null, false, false),
                    Text = "Load Saved Game",
                },
            };
        }
    }
}
