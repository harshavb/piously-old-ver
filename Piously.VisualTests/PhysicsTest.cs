using System;
using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.Physics;
using osu.Framework.Graphics.Shapes;
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
        public void DropCubeTest()
        {
            AddStep("Drop a cube", performDropCube); // Description, callback
        }

        private void performDropCube()
        {
            // Add a new cube to the simulation
            RigidBodyContainer<Drawable> rbc = new RigidBodyContainer<Drawable>
            {
                Child = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(150, 150),
                    Colour = Color4.Tomato,
                },
                Position = new Vector2(500, 200),
                Size = new Vector2(150, 150),
                Rotation = 45,
                Colour = Color4.Tomato,
                Masking = true,
                Restitution = 1.01f,
            };

            sim.Add(rbc);
        }
    }
}