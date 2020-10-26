using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Graphics.Cursor
{
    public class PiouslyContextMenuContainer : ContextMenuContainer
    {
        protected override Menu CreateMenu() => new PiouslyContextMenu();
    }
}
