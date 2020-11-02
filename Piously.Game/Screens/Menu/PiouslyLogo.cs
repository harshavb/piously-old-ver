using System;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Audio.Track;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Backgrounds;
using Piously.Game.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;
using Piously.Game.Graphics.Primitives;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Piously.Game.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class PiouslyLogo : Container
    {
        private const double transition_length = 300;

        private Texture logo;

        private readonly Container colourAndHexagons;
        private readonly Hexagons hexagons;

        public bool Hexagons
        {
            set => colourAndHexagons.FadeTo(value ? 1 : 0, transition_length, Easing.OutQuint);
        }

        public PiouslyLogo()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;

            Children = new Drawable[]
            {
                new Graphics.Shapes.Hexagon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(200, 200),
                    Colour = Color4.Tomato
                },
                new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Texture = logo
                },
                colourAndHexagons = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = PiouslyColour.PiouslyYellow,
                        },
                        hexagons = new Hexagons
                        {
                            HexagonScale = 4,
                            ColourLight = Color4Extensions.FromHex(@"ff7db7"),
                            ColourDark = Color4Extensions.FromHex(@"de5b95"),
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            };
        }

        private void load(TextureStore textures)
        {
            logo = textures.Get(@"logo");
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
