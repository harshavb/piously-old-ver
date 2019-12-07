using System;
using osu.Framework;
using osu.Framework.Platform;
//using Piously.Game;
using Piously.VisualTests;

namespace Piously.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"piously"))
            //using (osu.Framework.Game game = new PiouslyGame())
            using (osu.Framework.Game game = new VisualTestRunner()) // Creates the visual test browser
                host.Run(game);
        }
    }
}