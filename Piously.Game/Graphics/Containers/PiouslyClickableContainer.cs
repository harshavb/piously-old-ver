using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Graphics.Containers
{
    public class PiouslyClickableContainer : ClickableContainer, IHasTooltip
    {
        private readonly HoverSampleSet sampleSet;

        private readonly Container content = new Container { RelativeSizeAxes = Axes.Both };

        protected override Container<Drawable> Content => content;

        protected virtual HoverClickSounds CreateHoverClickSounds(HoverSampleSet sampleSet) => new HoverClickSounds(sampleSet);

        public PiouslyClickableContainer(HoverSampleSet sampleSet = HoverSampleSet.Normal)
        {
            this.sampleSet = sampleSet;
        }

        public virtual string TooltipText { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (AutoSizeAxes != Axes.None)
            {
                content.RelativeSizeAxes = (Axes.Both & ~AutoSizeAxes);
                content.AutoSizeAxes = AutoSizeAxes;
            }

            InternalChildren = new Drawable[]
            {
                content,
                CreateHoverClickSounds(sampleSet)
            };
        }
    }
}
