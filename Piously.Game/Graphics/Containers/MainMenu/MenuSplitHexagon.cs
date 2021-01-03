using System;
using osu.Framework.Graphics.Containers;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Graphics.Containers.MainMenu
{
    public class MenuSplitHexagon : Container<MenuButton>
    {
        public MainMenuContainer parentLogo;
        public MenuButton[] triangles { get; private set; }

        public Action OnSettings;
        public Action OnExit;
        public Action OnLocalGame;
        public Action OnPlay;

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = false;
            CreateTriangles();
        }

        private void CreateTriangles()
        {
            triangles = new MenuButton[6];
            for (int i = 0; i < 6; ++i)
            {
                Add(triangles[i] = new MenuButton
                {
                    triangleColour = PiouslyColour.Gray(75),
                    clickAction = i switch
                    {
                        0 => () => OnExit?.Invoke(),
                        1 => () => OnSettings?.Invoke(),
                        2 => () => OnPlay?.Invoke(),
                        3 => () => OnPlay?.Invoke(),
                        4 => () => OnLocalGame?.Invoke(),
                        5 => () => OnPlay?.Invoke(),
                        _ => () => OnPlay?.Invoke(),
                    },
                    titleText = i switch
                    {
                        0 => "Exit",
                        1 => "Settings",
                        2 => "Singleplayer",
                        3 => "Multiplayer",
                        4 => "Local Game",
                        5 => "Leaderboard",
                        _ => "",
                    },
                    textIsUpsideDown = i switch
                    {
                        0 => true,
                        1 => true,
                        2 => false,
                        3 => false,
                        4 => false,
                        5 => true,
                        _ => false,
                    },
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f),
                    Rotation = i * 60 + Rotation,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.TopCentre,
                    parentLogo = parentLogo,
                    Masking = false,
                });
            }
        }

        public void ScaleTo(float newScale, double duration = 0, Easing Easing = Easing.None)
        {
            foreach(MenuButton menuButton in Children)
            {
                menuButton.ScaleTo(newScale, duration, Easing);
            }
        }
    }
}
