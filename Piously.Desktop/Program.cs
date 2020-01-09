using System;
using osu.Framework;
using osu.Framework.Platform;
//using Piously.Game;
//using Piously.PhysicsTests;
using Piously.MenuTests;


namespace Piously.Desktop
{
    public static class Program
    {
        [STAThread] // Necessary for GUIs on Windows, else weird stuff happens
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"piously"))
            // using (osu.Framework.Game game = new PiouslyGame())
            using (osu.Framework.Game game = new MenuTestRunner()) // Instead of using regular game (PiouslyGame.cs), using the test browser (PhysicsTestRunner.cs)
            host.Run(game);
        }
    }
}