using System;
using System.Linq;
using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Testing;
using osu.Framework.Physics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osuTK;
using osuTK.Graphics;

using Piously.Game.Input;

namespace Piously.PhysicsTests
{
    [TestFixture] // Needed for NUnit
    public class PhysicsTest : TestScene // TestScene has the AddStep function
    {
        // TODO:
        // Figure out a way to make NUnit test functions that take parameters from the user during runtime
        // For example, create a random cube with desired restitution/color/size that can be supplemented in the browser
        private RigidBodySimulation sim;
        private class PiouslyTestKeyBindingReceptor : Drawable, IKeyBindingHandler<InputAction> // IKeyBindingHandler implements OnPressed and OnReleased and passes the InputAction enum
        {
            private Player player; // Drawable object that will be affected
            private RigidBodySimulation sim; // Simulation that Player will be in
            private InputAction key;

            // Creates a new player, and adds it to RigidBodySimulation sim
            public PiouslyTestKeyBindingReceptor(RigidBodySimulation sim)
            {
                this.player = new Player();
                this.sim = sim;
                this.sim.Add(this.player);
            }

            // Called when a key is pressed
            public bool OnPressed(InputAction action)
            {
                switch (action)
                {
                    case InputAction.Jump:
                        player.Velocity = new Vector2(player.constantXForce, player.Velocity.Y - 500); // Give an upwards velocity
                        break;
                    case InputAction.Right:
                        player.constantXForce += Player.PLAYER_VELOCITY; // Give a rightwards velocity
                        break;
                    case InputAction.Left:
                        player.constantXForce -= Player.PLAYER_VELOCITY; // Give a leftwards velocity
                        break;
                }
                this.key = action;
                return true; // Returns true when the event has been handled
            }

            // Called when a key is released
            public bool OnReleased(InputAction action)
            {
                switch (action) // The following actions reset the players velocity to zero when the left or right key is released. This allows for the player to hold left or right (but not jump) to key moving in that direction
                {
                    case InputAction.Left:
                        player.constantXForce += Player.PLAYER_VELOCITY;
                        break;
                    case InputAction.Right:
                        player.constantXForce -= Player.PLAYER_VELOCITY;
                        break;
                }
                return true;
            }
        }

        private class Player : RigidBodyContainer<Drawable>
        {
            public float constantXForce; // Stores x-ward velocity

            [BackgroundDependencyLoader]
            private void load(TextureStore textureStore)
            {
                Child = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(200, 200),
                };
                Position = new Vector2(200, 100);
                Size = new Vector2(200, 200);
                Masking = true;
            }

            public const float PLAYER_VELOCITY = 2000f; // How fast it moves left or right

            protected override void Update() // Updates velocity with each tick
            {
                base.Update();
                Velocity = new Vector2(constantXForce, Velocity.Y);
            }
        }
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
        [Test]
        public void NewPlayerTest()
        {
            AddStep("Add a player", createNewPlayer);
        }
        private void createNewPlayer()
        {
            PiouslyKeyBindingContainer keyBindingContainer = new PiouslyKeyBindingContainer(); // Create an object that detects key presses and releases
            PiouslyTestKeyBindingReceptor keyBindingReceptor = new PiouslyTestKeyBindingReceptor(sim); // Create an object that listens to when keyBindingContainer changes the InputAction
            keyBindingContainer.Child = keyBindingReceptor; // Sets the receptor as the child of the detector, as is required
            Add(keyBindingContainer); // Add the keyBindingDetector to the scene
        }
        private void performDropRect() // Adds a rectangle of random properties to the simulation
        {
            Random rand = new Random();
            int x = rand.Next(50, 201); // width
            int y = rand.Next(50, 201); // height
            Console.WriteLine(Axes.X);
            int posx = rand.Next(200, 1201); // x-position
            int posy = rand.Next(0, 500); // y-position
            byte r = (byte)rand.Next(100, 256); // red channel
            byte g = (byte)rand.Next(100, 256); // green channel
            byte b = (byte)rand.Next(100, 256); // blue channel
            float rt = (float)rand.NextDouble() * 2.05f - 1; // restitution
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
                Text = rt.ToString().Substring(0, 5), // Displays the restitution to two decimal places
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Color4.Black,
                Font = new FontUsage(null, Math.Min(x*0.5f, y*0.5f)), // Scales the font, but only really works with four characters
                // TODO:
                // Figure out how to make font scale to container size, no matter text size
            };

            if(rt >= 0) // If the number is positive, adjust precisison for one less character (-)
            {
                txt.Text = txt.Text.ToString().Substring(0, 4);
            }

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
