using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
namespace Piously.Game.Graphics.Containers.LocalGame.LoadGame
{
    public class SaveFileListContainer : PiouslyScrollContainer
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(0.95f, 0.4f);
            Position = new Vector2(0f, 0.2f);

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = new PiouslyColour().Gray4,
                },
            };
        }
    }
}
