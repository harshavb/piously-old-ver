using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Piously.Game.Graphics.Containers
{
    public class ReverseChildIDFillFlowContainer<T> : FillFlowContainer<T> where T : Drawable
    {
        protected override int Compare(Drawable x, Drawable y) => CompareReverseChildID(x, y);
    }
}
