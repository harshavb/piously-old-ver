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
                    Colour = Color4.Tomato
                },
                new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(500, 500),
                    Texture = logo
                }
            };
        }

        private void load(TextureStore textures, AudioManager audio)
        {
            logo = textures.Get(@"logo");
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
