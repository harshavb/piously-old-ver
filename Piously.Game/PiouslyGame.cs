using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Physics;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game
{
    public class PiouslyGame : osu.Framework.Game
    {
        RigidBodySimulation sim;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = sim = new RigidBodySimulation { RelativeSizeAxes = Axes.Both };

            RigidBodyContainer<Drawable> rbc = new RigidBodyContainer<Drawable>
            {
                Child = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(150, 150),
                    Colour = Color4.Tomato,
                },
                Position = new Vector2(500, 500),
                Size = new Vector2(200, 200),
                Rotation = 45,
                Colour = Color4.Tomato,
                Masking = true,
            };

            sim.Add(rbc);
        }
    }
}