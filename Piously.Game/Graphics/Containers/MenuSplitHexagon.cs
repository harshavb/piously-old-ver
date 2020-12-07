﻿using System;
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
                    triangleColour = i switch
                    {
                        0 => new Colour4(75, 75, 75, 255),
                        1 => new Colour4(75, 75, 75, 255),
                        2 => new Colour4(75, 75, 75, 255),
                        3 => new Colour4(75, 75, 75, 255),
                        4 => new Colour4(75, 75, 75, 255),
                        5 => new Colour4(75, 75, 75, 255),
                        _ => new Colour4(75, 75, 75, 255),
                    },
                    clickAction = i switch
                    {
                        0 => () => OnExit?.Invoke(),
                        1 => () => OnSettings?.Invoke(),
                        2 => () => OnLocalGame?.Invoke(),
                        3 => () => OnPlay?.Invoke(),
                        4 => () => OnPlay?.Invoke(),
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
