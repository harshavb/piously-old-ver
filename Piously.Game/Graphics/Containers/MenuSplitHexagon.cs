using System;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class MenuSplitHexagon : Container<MenuButton>
    {
        public MenuLogo parentLogo;
        public MenuButton[] triangles { get; private set; }

        public Action OnSettings;
        public Action OnExit;

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
                    triangleColour = i switch
                    {
                        0 => Colour4.DarkGray,
                        1 => Colour4.DarkGray,
                        2 => Colour4.DarkGray,
                        3 => Colour4.DarkGray,
                        4 => Colour4.DarkGray,
                        5 => Colour4.DarkGray,
                        _ => Colour4.DarkGray,
                    },
                    clickAction = i switch
                    {
                        0 => () => OnExit?.Invoke(),
                        1 => () => OnSettings?.Invoke(),
                        2 => () => OnSettings?.Invoke(),
                        3 => () => OnSettings?.Invoke(),
                        4 => () => OnSettings?.Invoke(),
                        5 => () => OnSettings?.Invoke(),
                        _ => () => OnSettings?.Invoke(),
                    },
                    titleText = i switch
                    {
                        0 => "Exit",
                        1 => "Settings",
                        2 => "Singleplayer",
                        3 => "Local Game",
                        4 => "Online Game",
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
