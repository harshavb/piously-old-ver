using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Piously.Game.Graphics.Backgrounds;

namespace Piously.Game.Graphics.UserInterface
{
    public class HexagonButton : PiouslyButton, IFilterable
    {
        protected Hexagons Hexagons { get; private set; }

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colours)
        {
            Add(Hexagons = new Hexagons
            {
                RelativeSizeAxes = Axes.Both,
                ColourDark = colours.BlueDarker,
                ColourLight = colours.Blue,
            });
        }

        public virtual IEnumerable<string> FilterTerms => new[] { Text };

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1 : 0);
        }

        public bool FilteringActive { get; set; }
    }
}
