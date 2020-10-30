using System;
using osu.Framework;
using osu.Framework.Platform;
using Piously.Game;

namespace Piously.Desktop
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"Piously"))
            using (osu.Framework.Game game = new PiouslyGame())
                host.Run(game);
        }
    }
}
