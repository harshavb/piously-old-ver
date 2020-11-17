using osu.Framework.Input.Events;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics.Shapes;
using Piously.Game.Screens.Menu;
using osuTK;
using System;

namespace Piously.Game.Graphics.Containers
{
    public class MenuLogo : HexagonalContainer
    {
        public PiouslyLogo logo;
        private MenuTriangle testTriangle0;
        private MenuTriangle testTriangle1;
        private MenuTriangle testTriangle2;
        private MenuTriangle testTriangle3;
        private MenuTriangle testTriangle4;
        private MenuTriangle testTriangle5;

        public MenuLogo()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(1200, 1200);

            Children = new Drawable[] {
                    testTriangle0 = new MenuTriangle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(0.5f * 0.667f),
                        Rotation = 180,
                        Colour = Colour4.Fuchsia,
                        parentLogo = this,
                    },
                    testTriangle1 = new MenuTriangle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(0.5f * 0.667f),
                        Rotation = 240,
                        Colour = Colour4.RoyalBlue,
                        parentLogo = this,
                    },
                    testTriangle2 = new MenuTriangle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(0.5f * 0.667f),
                        Rotation = 300,
                        Colour = Colour4.Orange,
                        parentLogo = this,
                    },
                    testTriangle3 = new MenuTriangle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(0.5f * 0.667f),
                        Rotation = 0,
                        Colour = Colour4.DarkSeaGreen,
                        parentLogo = this,
                    },
                    testTriangle4 = new MenuTriangle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(0.5f * 0.667f),
                        Rotation = 60,
                        Colour = Colour4.Maroon,
                        parentLogo = this,
                    },
                    testTriangle5 = new MenuTriangle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(0.5f * 0.667f),
                        Rotation = 120,
                        Colour = Colour4.SandyBrown,
                        parentLogo = this,
                    },
                    logo = new PiouslyLogo()
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(1f * 0.667f),
                        parentLogo = this,
                    },
            };
        }

        public void toggleButtons()
        {
            if(logo.menuState == MenuState.Closed)
            {
                Console.WriteLine("ok now this is epic");
                testTriangle0.ScaleTo(1f, 300, Easing.InOutBounce);
                testTriangle1.ScaleTo(1f, 300, Easing.InOutBounce);
                testTriangle2.ScaleTo(1f, 300, Easing.InOutBounce);
                testTriangle3.ScaleTo(1f, 300, Easing.InOutBounce);
                testTriangle4.ScaleTo(1f, 300, Easing.InOutBounce);
                testTriangle5.ScaleTo(1f, 300, Easing.InOutBounce);
            }
            else if(logo.menuState == MenuState.Opened)
            {
                testTriangle0.ScaleTo(1.375f, 300, Easing.InOutBounce);
                testTriangle1.ScaleTo(1.375f, 300, Easing.InOutBounce);
                testTriangle2.ScaleTo(1.375f, 300, Easing.InOutBounce);
                testTriangle3.ScaleTo(1.375f, 300, Easing.InOutBounce);
                testTriangle4.ScaleTo(1.375f, 300, Easing.InOutBounce);
                testTriangle5.ScaleTo(1.375f, 300, Easing.InOutBounce);
            }
        }

        public void checkTriangleHovers()
        {
            if(testTriangle0.IsHovered && logo.menuState == MenuState.Opened)
            {
                testTriangle0.ScaleTo(1.5f, 100, Easing.InOutBounce);
            }
            else if (testTriangle1.IsHovered && logo.menuState == MenuState.Opened)
            {
                testTriangle1.ScaleTo(1.5f, 100, Easing.InOutBounce);
            }
            else if (testTriangle2.IsHovered && logo.menuState == MenuState.Opened)
            {
                testTriangle2.ScaleTo(1.5f, 100, Easing.InOutBounce);
            }
            else if (testTriangle3.IsHovered && logo.menuState == MenuState.Opened)
            {
                testTriangle3.ScaleTo(1.5f, 100, Easing.InOutBounce);
            }
            else if (testTriangle4.IsHovered && logo.menuState == MenuState.Opened)
            {
                testTriangle4.ScaleTo(1.5f, 100, Easing.InOutBounce);
            }
            else if (testTriangle5.IsHovered && logo.menuState == MenuState.Opened)
            {
                testTriangle5.ScaleTo(1.5f, 100, Easing.InOutBounce);
            }
        }

        public void closeAllTriangles()
        {
            if (logo.menuState == MenuState.Opened)
            {
                testTriangle0.ScaleTo(1.375f, 100, Easing.InOutBounce);
                testTriangle1.ScaleTo(1.375f, 100, Easing.InOutBounce);
                testTriangle2.ScaleTo(1.375f, 100, Easing.InOutBounce);
                testTriangle3.ScaleTo(1.375f, 100, Easing.InOutBounce);
                testTriangle4.ScaleTo(1.375f, 100, Easing.InOutBounce);
                testTriangle5.ScaleTo(1.375f, 100, Easing.InOutBounce);
            }
        }

        protected override bool OnHover(HoverEvent e)
        {
            if(logo.menuState == MenuState.Opened)
                this.ScaleTo(1.1f, 50);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if(logo.menuState == MenuState.Opened)
                this.ScaleTo(1f, 50);
        }

    }
}
