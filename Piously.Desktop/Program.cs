﻿using System;
using osu.Framework;
using osu.Framework.Platform;
using Piously.Game;

namespace Piously.Desktop
{
    //The actual file which runs the game
    public static class Program
    {
        [STAThread]
        public static int Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"Piously"))
                host.Run(new PiouslyGameDesktop());

            return 0;
        }
    }
}
