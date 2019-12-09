using System;
using System.Threading.Tasks;
using System.Linq;
using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Physics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Text;
using osu.Framework.Graphics.Sprites;
using osu.Framework;
using osuTK;
using osuTK.Graphics;

namespace Piously.VisualTests
{
    [TestFixture] // Needed for NUnit
    public class PhysicsTest : TestScene // TestScene has the AddStep function
    {
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
        private void performDropRect()
        {
            // Add a new cube to the simulation
            Random rand = new Random();
            int x = rand.Next(50, 201);
            int y = rand.Next(50, 201);
            byte r = (byte)rand.Next(0, 256);
            byte g = (byte)rand.Next(0, 256);
            byte b = (byte)rand.Next(0, 256);
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
                Position = new Vector2(rand.Next(200, 1201), rand.Next(0, 500)),
                Size = new Vector2(x, y),
                Rotation = 0,
                Colour = color,
                Masking = true,
                Restitution = 1.01f,
            };

            sim.Add(rbc);
        }
        private void removeAllObjects()
        {
            for(int i = sim.Count - 1; i >= 0; i--)
            {
                sim.Remove(sim.ElementAt(i));
            }
        }
    }
}