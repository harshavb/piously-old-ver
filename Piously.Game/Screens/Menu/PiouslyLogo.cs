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
using Piously.Game.Graphics;
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
    public class PiouslyLogo : TestClickableContainer
    {
        private const double transition_length = 300;

        private Sprite logo;

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
            Size = new Vector2(800, 800);

            Children = new Drawable[]
            {
                new HexagonalContainer()
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Scale = new Vector2(0.95f),
                    Child = colourAndHexagons = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Children = new Drawable[]
                        {
                            new Graphics.Shapes.Hexagon
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = PiouslyColour.PiouslyLightYellow,
                            },
                            hexagons = new Hexagons
                            {
                                HexagonScale = 3,
                                ColourLight = PiouslyColour.PiouslyLighterYellow,
                                ColourDark = PiouslyColour.PiouslyYellow,
                                RelativeSizeAxes = Axes.Both,
                            }
                        }
                    }
                },
                logo = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fit,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            logo.Texture = textures.Get(@"logo");
        }
    }
}
