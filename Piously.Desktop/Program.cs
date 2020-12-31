using System;
using osu.Framework;
using osu.Framework.Platform;
using Sentry;

namespace Piously.Desktop
{
    //The actual file which runs the game
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {

            using (SentrySdk.Init("https://2f1d113b6b3248f684c0b39dd39cd96a@o468392.ingest.sentry.io/5576208"))
            using (GameHost host = Host.GetSuitableHost(@"Piously", useOsuTK: false))
            {
                host.Run(new PiouslyGameDesktop());
            }

            return 0;
        }
    }
}
