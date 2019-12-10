using System;
using System.Linq;
using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Physics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace Piously.VisualTests
{
    [TestFixture] // Needed for NUnit
    public class PhysicsTest : TestScene // TestScene has the AddStep function
    {
        // TODO:
        // Figure out a way to make NUnit test functions that take parameters from the user during runtime
        // For example, create a random cube with desired restitution/color/size that can be supplemented in the browser
        private RigidBodySimulation sim;

        [BackgroundDependencyLoader]
        private void load()
        {
            // Set up the simulation once before any tests are ran
            Child = sim = new RigidBodySimulation { RelativeSizeAxes = Axes.Both };
        }

        [Test] // Marks tests for NUnit
        public void DropRectTest()
        {
            AddStep("Drop a rectangle", performDropRect); // Description, callback
        }
        [Test]
        public void RemoveAllObjectTest()
        {
            AddStep("Remove all objects", removeAllObjects);
        }
        private void performDropRect() // Adds a rectangle of random properties to the simulation
        {
            Random rand = new Random();
            int x = rand.Next(50, 201); // width
            int y = rand.Next(50, 201); // height
            int posx = rand.Next(200, 1201); // x-position
            int posy = rand.Next(0, 500); // y-position
            byte r = (byte)rand.Next(100, 256); // red channel
            byte g = (byte)rand.Next(100, 256); // green channel
            byte b = (byte)rand.Next(100, 256); // blue channel
            float rt = (float)rand.NextDouble(); // restitution
            Color4 color = new Color4(r, g, b, 255);
            RigidBodyContainer<Drawable> rbc = new RigidBodyContainer<Drawable>
            {
                Child = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(x, y),
                    Colour = color,
                },
                Position = new Vector2(posx, posy),
                Size = new Vector2(x, y),
                Rotation = 0,
                Colour = color,
                Masking = true,
                Restitution = rt,
            };

            SpriteText txt = new SpriteText
            {
                Text = rt.ToString().Substring(0, 4), // Displays the restitution to two decimal places
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Color4.Black,
                Font = new FontUsage(null, Math.Min(x*0.6f, y*0.6f)), // Scales the font, but only really works with four characters
                // TODO:
                // Figure out how to make font scale to container size, no matter text size
            };

            rbc.Add(txt);
            sim.Add(rbc);
        }
        private void removeAllObjects() // Destroys all objects in the simulation
        {
            for(int i = sim.Count - 1; i >= 0; i--) // Loops backwards; looping forwards would skip objects after a removal
            {
                sim.Remove(sim.ElementAt(i));
            }
        }
    }
}
