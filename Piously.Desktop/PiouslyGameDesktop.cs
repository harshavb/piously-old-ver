using osu.Framework.Platform;
using Piously.Game;

namespace Piously.Desktop
{
    //Sets up various settings and manages installation for Desktop users
    internal class PiouslyGameDesktop : PiouslyGame
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            host.Window.WindowState = WindowState.Maximised;

            //host.Window.CursorState |= CursorState.Hidden;
            host.Window.Title = Name;
        }
    }
}
