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
            Masking = true;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(0.5f, 1f);
            Position = new Vector2(0f, 0f);
            Children = new Drawable[] {

                // CreateGame
                new LocalGameButton
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(-0.1f, 0.2f),
                    Colour = colour.Pink,
                    Text = "Create Game",
                    Action = OnCreateGame,
                },

                // LoadGame
                new LocalGameButton
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(-0.1f, 0.4f),
                    Colour = colour.Pink,
                    Text = "Load Saved Game",
                    Action = OnLoadSavedGame,
                },
            };
        }
    }
}
