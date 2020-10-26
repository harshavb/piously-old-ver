using System;
using osu.Framework.Graphics;

namespace Piously.Game.Overlays.Settings
{
    public interface ISettingsItem : IDrawable, IDisposable
    {
        event Action SettingChanged;
    }
}
