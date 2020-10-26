using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Online.API.Requests.Responses;
using System;

namespace Piously.Game.Overlays.Changelog
{
    public class ChangelogContent : FillFlowContainer
    {
        public Action<APIChangelogBuild> BuildSelected;

        public void SelectBuild(APIChangelogBuild build) => BuildSelected?.Invoke(build);

        public ChangelogContent()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;
        }
    }
}
