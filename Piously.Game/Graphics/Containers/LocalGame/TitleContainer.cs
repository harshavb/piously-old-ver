using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class TitleContainer : Container
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            RelativePositionAxes = Axes.Both;
            Position = new Vector2(0.025f, 0.05f);

            Child = new SpriteText
            {
                Font = new FontUsage("Aller", 64, "Bold", false, false),
                Text = "Local Game",
            };
        }
    }
}
