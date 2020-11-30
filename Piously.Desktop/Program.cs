using System;
using osu.Framework;
using osu.Framework.Platform;

namespace Piously.Desktop
{
    //The actual file which runs the game
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"Piously", useOsuTK: true))
                host.Run(new PiouslyGameDesktop());

            return 0;
        }
    }
}
