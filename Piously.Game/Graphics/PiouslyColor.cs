using osu.Framework.Extensions.Color4Extensions;
using osuTK.Graphics;

namespace Piously.Game.Graphics
{
    public class PiouslyColor
    {
        public static Color4 Gray(float amt) => new Color4(amt, amt, amt, 1f);
        public static Color4 Gray(byte amt) => new Color4(amt, amt, amt, 255);

        public readonly Color4 PinkLighter = Color4Extensions.FromHex(@"ffddee");
        public readonly Color4 PinkLight = Color4Extensions.FromHex(@"ff99cc");
        public readonly Color4 Pink = Color4Extensions.FromHex(@"ff66aa");
        public readonly Color4 PinkDark = Color4Extensions.FromHex(@"cc5288");
        public readonly Color4 PinkDarker = Color4Extensions.FromHex(@"bb1177");

        public readonly Color4 GreenLight = Color4Extensions.FromHex(@"b3d944");
        public readonly Color4 GreenDarker = Color4Extensions.FromHex(@"445500");

        public readonly Color4 BlueLighter = Color4Extensions.FromHex(@"ddffff");
        public readonly Color4 BlueLight = Color4Extensions.FromHex(@"99eeff");
        public readonly Color4 Blue = Color4Extensions.FromHex(@"66ccff");
        public readonly Color4 BlueDark = Color4Extensions.FromHex(@"44aadd");
        public readonly Color4 BlueDarker = Color4Extensions.FromHex(@"2299bb");

        public readonly Color4 YellowLight = Color4Extensions.FromHex(@"ffdd55");
        public readonly Color4 Yellow = Color4Extensions.FromHex(@"ffcc22");
        public readonly Color4 YellowDark = Color4Extensions.FromHex(@"eeaa00");

        public readonly Color4 Red = Color4Extensions.FromHex(@"ed1121");
        public readonly Color4 RedDark = Color4Extensions.FromHex(@"ba0011");

        public readonly Color4 Green = Color4Extensions.FromHex(@"88b300");

        public readonly Color4 GreyVioletLighter = Color4Extensions.FromHex(@"ebb8fe");
        public readonly Color4 GreyVioletLight = Color4Extensions.FromHex(@"685370");

        public readonly Color4 GreySeafoamDark = Color4Extensions.FromHex(@"2c3532");
        public readonly Color4 GreySeafoamDarker = Color4Extensions.FromHex(@"1e2422");

        public readonly Color4 Gray1 = Color4Extensions.FromHex(@"111");
        public readonly Color4 Gray2 = Color4Extensions.FromHex(@"222");
        public readonly Color4 Gray3 = Color4Extensions.FromHex(@"333");
        public readonly Color4 Gray4 = Color4Extensions.FromHex(@"444");
        public readonly Color4 Gray5 = Color4Extensions.FromHex(@"555");
        public readonly Color4 Gray7 = Color4Extensions.FromHex(@"777");
        public readonly Color4 Gray8 = Color4Extensions.FromHex(@"888");
        public readonly Color4 Gray9 = Color4Extensions.FromHex(@"999");
        public readonly Color4 GrayF = Color4Extensions.FromHex(@"fff");

        public readonly Color4 ChatBlue = Color4Extensions.FromHex(@"17292e");

        public readonly Color4 ContextMenuGray = Color4Extensions.FromHex(@"223034");
    }
}
