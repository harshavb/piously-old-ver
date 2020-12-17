using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class LeftPanelContainer : Container
    {
        public Action OnCreateGame;
        public Action OnLoadSavedGame;

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colour)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(750, 677);
            Position = new Vector2(0f, 0f);
            Children = new Drawable[] {

                // CreateGame
                new LocalGameButton
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(-2.3f, 0.25f),
                    Colour = colour.Pink,
                    Text = "Create Game",
                    Action = OnCreateGame,
                },

                // LoadGame
                new LocalGameButton
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(-2.3f, 0.45f),
                    Colour = colour.Pink,
                    Text = "Load Saved Game",
                    Action = OnLoadSavedGame,
                },
            };
        }
    }
}
