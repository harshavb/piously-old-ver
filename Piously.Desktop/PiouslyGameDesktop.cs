using osu.Framework.Configuration;
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

            for(int i = 0; i < host.Window.SupportedWindowModes.Count; i++)
            {
                if (host.Window.SupportedWindowModes[i] == WindowMode.Borderless)
                    host.Window.WindowState = WindowState.FullscreenBorderless;
            }

            //host.Window.CursorState |= CursorState.Hidden;
            host.Window.Title = Name;
        }
    }
}
