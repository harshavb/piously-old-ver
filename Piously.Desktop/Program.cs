using System;
using osu.Framework;
using osu.Framework.Platform;
using Piously.Game;

//DON'T RUN TESTS IN THIS CLASS
namespace Piously.Desktop
{
    public static class Program
    {
        [STAThread] // Necessary for GUIs on Windows, else weird stuff happens
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"Piously"))
            using (osu.Framework.Game game = new PiouslyGame())
            host.Run(game);
        }
    }
}