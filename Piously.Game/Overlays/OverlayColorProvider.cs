using System;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Overlays
{
    public class OverlayColorProvider
    {
        private readonly OverlayColorScheme colourScheme;

        public OverlayColorProvider(OverlayColorScheme colourScheme)
        {
            this.colourScheme = colourScheme;
        }

        public Color4 Highlight1 => getColour(1, 0.7f);
        public Color4 Content1 => getColour(0.4f, 1);
        public Color4 Content2 => getColour(0.4f, 0.9f);
        public Color4 Light1 => getColour(0.4f, 0.8f);
        public Color4 Light2 => getColour(0.4f, 0.75f);
        public Color4 Light3 => getColour(0.4f, 0.7f);
        public Color4 Light4 => getColour(0.4f, 0.5f);
        public Color4 Dark1 => getColour(0.2f, 0.35f);
        public Color4 Dark2 => getColour(0.2f, 0.3f);
        public Color4 Dark3 => getColour(0.2f, 0.25f);
        public Color4 Dark4 => getColour(0.2f, 0.2f);
        public Color4 Dark5 => getColour(0.2f, 0.15f);
        public Color4 Dark6 => getColour(0.2f, 0.1f);
        public Color4 Foreground1 => getColour(0.1f, 0.6f);
        public Color4 Background1 => getColour(0.1f, 0.4f);
        public Color4 Background2 => getColour(0.1f, 0.3f);
        public Color4 Background3 => getColour(0.1f, 0.25f);
        public Color4 Background4 => getColour(0.1f, 0.2f);
        public Color4 Background5 => getColour(0.1f, 0.15f);
        public Color4 Background6 => getColour(0.1f, 0.1f);

        private Color4 getColour(float saturation, float lightness) => Color4.FromHsl(new Vector4(getBaseHue(colourScheme), saturation, lightness, 1));

        // See https://github.com/ppy/osu-web/blob/4218c288292d7c810b619075471eaea8bbb8f9d8/app/helpers.php#L1463
        private static float getBaseHue(OverlayColorScheme colourScheme)
        {
            switch (colourScheme)
            {
                default:
                    throw new ArgumentException($@"{colourScheme} colour scheme does not provide a hue value in {nameof(getBaseHue)}.");

                case OverlayColorScheme.Red:
                    return 0;

                case OverlayColorScheme.Pink:
                    return 333 / 360f;

                case OverlayColorScheme.Orange:
                    return 46 / 360f;

                case OverlayColorScheme.Green:
                    return 115 / 360f;

                case OverlayColorScheme.Purple:
                    return 255 / 360f;

                case OverlayColorScheme.Blue:
                    return 200 / 360f;
            }
        }
    }

    public enum OverlayColorScheme
    {
        Red,
        Pink,
        Orange,
        Green,
        Purple,
        Blue
    }
}
