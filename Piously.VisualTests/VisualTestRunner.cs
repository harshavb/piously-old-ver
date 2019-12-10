using osu.Framework.Platform;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Testing;

using Piously.Game;

namespace Piously.VisualTests
{
    public class VisualTestRunner : PiouslyGame
    {
        [BackgroundDependencyLoader] // Necessary for something
        private void load()
        {
            Child = new DrawSizePreservingFillContainer
            {
                Children = new Drawable[]
                {
                    new TestBrowser("Piously"), // Specify the namespace to discover tests from. Creates NUnit test browser
                    new CursorContainer(), // Enable cursor
                },
            };
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
