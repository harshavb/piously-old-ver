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
        Container con;
        byte changing = 0;
        bool inc = true;
        byte r = 255;
        byte g = 0;
        byte b = 0;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = con = new Container();
        }

        [Test]
        public void loadText()
        {
            AddStep("Load Text", newText);
        }

        private void newText()
        {
            SpriteText txt = new SpriteText
            {
                Text = "Piously",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = new Color4(255, 0, 0, 255),
                Font = new FontUsage(null, 50),
                Position = new Vector2(688, 334),
            };

            Action<Drawable> bruh = updateText;
            txt.OnUpdate += bruh;

            con.Add(txt);
            con.Show();
        }

        private void updateText(Drawable obj)
        {
            SpriteText x = (SpriteText)obj;
            obj.Rotation += 1;

            if (inc)
            {
                x.Font = new FontUsage(null, x.Font.Size + 1);
                if(x.Font.Size >= 200)
                {
                    inc = false;
                }
            } else
            {
                x.Font = new FontUsage(null, x.Font.Size - 1);
                if (x.Font.Size <= 12)
                {
                    inc = true;
                }
            }

            switch(changing)
            {
                case 0:
                    x.Colour = new Color4(r--, g++, b, 255);
                    if(r == 0)
                    {
                        changing = 1;
                    }
                    break;
                case 1:
                    x.Colour = new Color4(r, g--, b++, 255);
                    if(g == 0)
                    {
                        changing = 2;
                    }
                    break;
                case 2:
                    x.Colour = new Color4(r++, g, b--, 255);
                    if(b == 0)
                    {
                        changing = 0;
                    }
                    break;
            }

            if (x.Rotation >= 360)
            {
                x.Rotation -= 360;
            }
        }
    }
}
