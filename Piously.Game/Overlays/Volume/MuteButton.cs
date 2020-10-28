using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using Piously.Game.Graphics;
using Piously.Game.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Overlays.Volume
{
    public class MuteButton : PiouslyButton, IHasCurrentValue<bool>
    {
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

        private Color4 hoveredColour, unhoveredColour;

        private const float width = 100;
        public const float HEIGHT = 35;

        public MuteButton()
        {
            Content.BorderThickness = 3;
            Content.CornerRadius = HEIGHT / 2;
            Content.CornerExponent = 2;

            Size = new Vector2(width, HEIGHT);

            Action = () => Current.Value = !Current.Value;
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            hoveredColour = colors.YellowDark;

            Content.BorderColour = unhoveredColour = colors.Gray1;
            BackgroundColour = colors.Gray1;

            SpriteIcon icon;

            AddRange(new Drawable[]
            {
                icon = new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });

            Current.BindValueChanged(muted =>
            {
                icon.Icon = muted.NewValue ? FontAwesome.Solid.VolumeMute : FontAwesome.Solid.VolumeUp;
                icon.Size = new Vector2(muted.NewValue ? 18 : 20);
                icon.Margin = new MarginPadding { Right = muted.NewValue ? 2 : 0 };
            }, true);
        }

        protected override bool OnHover(HoverEvent e)
        {
            Content.TransformTo<Container<Drawable>, SRGBColour>("BorderColour", hoveredColour, 500, Easing.OutQuint);
            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            Content.TransformTo<Container<Drawable>, SRGBColour>("BorderColour", unhoveredColour, 500, Easing.OutQuint);
        }
    }
}
