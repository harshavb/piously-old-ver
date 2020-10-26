using System;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;

namespace Piously.Game.Graphics.UserInterface
{
    public class Nub : CircularContainer, IHasCurrentValue<bool>, IHasAccentColor
    {
        public const float COLLAPSED_SIZE = 20;
        public const float EXPANDED_SIZE = 40;

        private const float border_width = 3;

        public Nub()
        {
            Box fill;

            Size = new Vector2(COLLAPSED_SIZE, 12);

            BorderColour = Color4.White;
            BorderThickness = border_width;

            Masking = true;

            Children = new[]
            {
                fill = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    AlwaysPresent = true,
                },
            };

            Current.ValueChanged += filled =>
            {
                if (filled.NewValue)
                    fill.FadeIn(200, Easing.OutQuint);
                else
                    fill.FadeTo(0.01f, 200, Easing.OutQuint); //todo: remove once we figure why containers aren't drawing at all times
            };
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            AccentColor = colors.Pink;
            GlowingAccentColor = colors.PinkLighter;
            GlowColor = colors.PinkDarker;

            EdgeEffect = new EdgeEffectParameters
            {
                Colour = GlowColor,
                Type = EdgeEffectType.Glow,
                Radius = 10,
                Roundness = 8,
            };
        }

        protected override void LoadComplete()
        {
            FadeEdgeEffectTo(0);
        }

        private bool glowing;

        public bool Glowing
        {
            get => glowing;
            set
            {
                glowing = value;

                if (value)
                {
                    this.FadeColour(GlowingAccentColor, 500, Easing.OutQuint);
                    FadeEdgeEffectTo(1, 500, Easing.OutQuint);
                }
                else
                {
                    FadeEdgeEffectTo(0, 500);
                    this.FadeColour(AccentColor, 500);
                }
            }
        }

        public bool Expanded
        {
            set => this.ResizeTo(new Vector2(value ? EXPANDED_SIZE : COLLAPSED_SIZE, 12), 500, Easing.OutQuint);
        }

        private readonly Bindable<bool> current = new Bindable<bool>();

        public Bindable<bool> Current
        {
            get => current;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                current.UnbindBindings();
                current.BindTo(value);
            }
        }

        private Color4 accentColor;

        public Color4 AccentColor
        {
            get => accentColor;
            set
            {
                accentColor = value;
                if (!Glowing)
                    Colour = value;
            }
        }

        private Color4 glowingAccentColor;

        public Color4 GlowingAccentColor
        {
            get => glowingAccentColor;
            set
            {
                glowingAccentColor = value;
                if (Glowing)
                    Colour = value;
            }
        }

        private Color4 glowColor;

        public Color4 GlowColor
        {
            get => glowColor;
            set
            {
                glowColor = value;

                var effect = EdgeEffect;
                effect.Colour = value;
                EdgeEffect = effect;
            }
        }
    }
}
