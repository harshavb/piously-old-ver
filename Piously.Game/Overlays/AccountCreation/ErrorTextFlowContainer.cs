using System.Collections.Generic;
using osu.Framework.Graphics;
using Piously.Game.Graphics.Containers;
using osuTK.Graphics;

namespace Piously.Game.Overlays.AccountCreation
{
    public class ErrorTextFlowContainer : PiouslyTextFlowContainer
    {
        private readonly List<Drawable> errorDrawables = new List<Drawable>();

        public ErrorTextFlowContainer()
            : base(cp => cp.Font = cp.Font.With(size: 12))
        {
        }

        public void ClearErrors()
        {
            errorDrawables.ForEach(d => d.Expire());
        }

        public void AddErrors(string[] errors)
        {
            ClearErrors();

            if (errors == null) return;

            foreach (var error in errors)
                errorDrawables.AddRange(AddParagraph(error, cp => cp.Colour = Color4.Red));
        }
    }
}
