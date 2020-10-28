using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace Piously.Game.Screens.Backgrounds
{
    public class BackgroundScreenBlack : BackgroundScreen
    {
        public BackgroundScreenBlack()
        {
            InternalChild = new Box
            {
                Colour = Color4.Black,
                RelativeSizeAxes = Axes.Both,
            };
        }

        public override void OnEntering(IScreen last)
        {
            Show();
        }
    }
}
