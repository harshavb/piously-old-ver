using System;
using System.Collections.Generic;
using System.Text;

namespace Piously.Game.Overlays
{
    public interface INamedOverlayComponent
    {
        string IconTexture { get; }

        string Title { get; }

        string Description { get; }
    }
}
