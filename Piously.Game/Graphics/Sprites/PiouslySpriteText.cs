using osu.Framework.Graphics.Sprites;

namespace Piously.Game.Graphics.Sprites
{
    public class PiouslySpriteText : SpriteText
    {
        public PiouslySpriteText()
        {
            Shadow = true;
            Font = PiouslyFont.Default;
        }
    }
}
