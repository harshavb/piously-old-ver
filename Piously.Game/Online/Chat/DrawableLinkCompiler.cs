using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.UserInterface;
using osuTK;

namespace Piously.Game.Online.Chat
{
    /// <summary>
    /// An invisible drawable that brings multiple <see cref="Drawable"/> pieces together to form a consumable clickable link.
    /// </summary>
    public class DrawableLinkCompiler : PiouslyHoverContainer
    {
        /// <summary>
        /// Each word part of a chat link (split for word-wrap support).
        /// </summary>
        public List<Drawable> Parts;

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => Parts.Any(d => d.ReceivePositionalInputAt(screenSpacePos));

        protected override HoverClickSounds CreateHoverClickSounds(HoverSampleSet sampleSet) => new LinkHoverSounds(sampleSet, Parts);

        public DrawableLinkCompiler(IEnumerable<Drawable> parts)
        {
            Parts = parts.ToList();
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            IdleColor = colors.Blue;
        }

        protected override IEnumerable<Drawable> EffectTargets => Parts;

        private class LinkHoverSounds : HoverClickSounds
        {
            private readonly List<Drawable> parts;

            public LinkHoverSounds(HoverSampleSet sampleSet, List<Drawable> parts)
                : base(sampleSet)
            {
                this.parts = parts;
            }

            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => parts.Any(d => d.ReceivePositionalInputAt(screenSpacePos));
        }
    }
}
