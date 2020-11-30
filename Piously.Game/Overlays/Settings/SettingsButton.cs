using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings
{
    public class SettingsButton : HexagonButton, IHasTooltip
    {
        public SettingsButton()
        {
            RelativeSizeAxes = Axes.X;
            Padding = new MarginPadding { Left = SettingsPanel.CONTENT_MARGINS, Right = SettingsPanel.CONTENT_MARGINS };
        }

        public string TooltipText { get; set; }

        public override IEnumerable<string> FilterTerms
        {
            get
            {
                if (TooltipText != null)
                    return base.FilterTerms.Append(TooltipText);

                return base.FilterTerms;
            }
        }
    }
}
