using System;
using osu.Framework;
using osu.Framework.Platform;

namespace Piously.PhysicsTests
{
    public static class Program
    {
        [STAThread] // Necessary for GUIs on Windows, else weird stuff happens
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"Piously"))
            using (osu.Framework.Game game = new PhysicsTestRunner())
                host.Run(game);
        }
    }
}