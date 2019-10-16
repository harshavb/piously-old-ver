using System;
using osu.Framework;
using osu.Framework.Platform;
using Piously.Game;

namespace Piously.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"awesome-game"))
            using (osu.Framework.Game game = new PiouslyGame())
                host.Run(game);
        }
    }
}