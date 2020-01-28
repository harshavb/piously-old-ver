using System;
using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Testing;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Screens;

using osuTK;
using osuTK.Graphics;
using Piously.Game;

namespace Piously.MenuTests
{
    [TestFixture]
    public class MenuTest : TestScene
    {

        public Container con;

        private class PiouslyText : SpriteText
        {
            public byte changing;
            public bool inc;
            public byte r;
            public byte g;
            public byte b;


            public PiouslyText()
            {
                this.changing = 0;
                this.inc = true;
                this.r = 255;
                this.g = 0;
                this.b = 0;
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = con = new Container();
        }

        [Test]
        public void bulgeText()
        {
            AddStep("Load bulging text", newBulgeText);
        }

        [Test]
        public void bounceTest()
        {
            AddStep("Load bouncing text", newBounceText);
        }

        [Test]
        public void clearText()
        {
            AddStep("Clear all text", clearAllText);
        }

        private void clearAllText()
        {
            for(int i = con.Count - 1; i >= 0; i--)
            {
                con.Remove(con[i]);
            }
        }
        private void newBulgeText()
        {
            PiouslyText txt = new PiouslyText
            {
                Text = "Piously",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = new Color4(255, 0, 0, 255),
                Font = new FontUsage(null, 50),
                Position = new Vector2(688, 334),
            };

            Action<Drawable> bruh = updateBulgeText;
            txt.OnUpdate += bruh;

            con.Add(txt);
            con.Show();
        }

        private void newBounceText()
        {
            PiouslyText txt = new PiouslyText
            {
                Text = "Piously",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = new Color4(255, 255, 255, 255),
                Font = new FontUsage(null, 50),
                Position = new Vector2(688, 334),
            };

            Action<Drawable> bruh = updateBounceText;
            txt.OnUpdate += bruh;

            con.Add(txt);
            con.Show();
        }

        private void updateBulgeText(Drawable obj)
        {
            PiouslyText x = (PiouslyText)obj;
            obj.Rotation += 1.5f;

            if (x.inc)
            {
                x.Font = new FontUsage(null, x.Font.Size * 1.02f);
                if(x.Font.Size >= 400)
                {
                    x.inc = false;
                }
            } else
            {
                x.Font = new FontUsage(null, x.Font.Size / 1.02f);
                if (x.Font.Size <= 12)
                {
                    x.inc = true;
                }
            }

            switch(x.changing)
            {
                case 0:
                    x.Colour = new Color4(x.r--, x.g++, x.b, 255);
                    if(x.r == 0)
                    {
                        x.changing = 1;
                    }
                    break;
                case 1:
                    x.Colour = new Color4(x.r, x.g--, x.b++, 255);
                    if(x.g == 0)
                    {
                        x.changing = 2;
                    }
                    break;
                case 2:
                    x.Colour = new Color4(x.r++, x.g, x.b--, 255);
                    if(x.b == 0)
                    {
                        x.changing = 0;
                    }
                    break;
            }

            if (x.Rotation >= 360)
            {
                x.Rotation -= 360;
            }
        }

        private void updateBounceText(Drawable obj)
        {
            PiouslyText x = (PiouslyText)obj;
            switch (x.changing)
            {
                case 0:
                    x.X += 1;
                    x.Y += 1;
                    if (x.X + x.Width / 2 >= this.LayoutRectangle.Width - con.ToParentSpace(new Vector2(0, 0)).X)
                    {
                        x.changing = 3;
                    }
                    else if (x.Y + x.Height / 2 >= this.LayoutRectangle.Height)
                    {
                        x.changing = 1;
                    }
                    break;
                case 1:
                    x.X += 1;
                    x.Y -= 1;
                    if (x.X + x.Width / 2 >= this.LayoutRectangle.Width - con.ToParentSpace(new Vector2(0, 0)).X)
                    {
                        x.changing = 2;
                    }
                    else if (x.Y - x.Height / 2 <= 0)
                    {
                        x.changing = 0;
                    }
                    break;
                case 2:
                    x.X -= 1;
                    x.Y -= 1;
                    if (x.X - x.Width / 2 <= 0)
                    {
                        x.changing = 1;
                    }
                    else if (x.Y - x.Height / 2 <= 0)
                    {
                        x.changing = 3;
                    }
                    break;
                case 3:
                    x.X -= 1;
                    x.Y += 1;
                    if (x.X - x.Width / 2 < 0)
                    {
                        x.changing = 0;
                    }
                    else if (x.Y + x.Height / 2 >= this.LayoutRectangle.Height)
                    {
                        x.changing = 2;
                    }
                    break;
            }
        }
    }
}
