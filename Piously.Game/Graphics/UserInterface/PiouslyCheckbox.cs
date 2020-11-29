using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyCheckbox : Checkbox
    {
        public Color4 CheckedColor { get; set; } = Color4.Cyan;
        public Color4 UncheckedColor { get; set; } = Color4.White;
        public int FadeDuration { get; set; }

        public string LabelText
        {
            set
            {
                if (labelText != null)
                    labelText.Text = value;
            }
        }

        public MarginPadding LabelPadding
        {
            get => labelText?.Padding ?? new MarginPadding();
            set
            {
                if (labelText != null)
                    labelText.Padding = value;
            }
        }

        protected readonly Nub Nub;

        private readonly TextFlowContainer labelText;

        public PiouslyCheckbox()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            const float nub_padding = 5;

            Children = new Drawable[]
            {
                labelText = new TextFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Padding = new MarginPadding { Right = Nub.EXPANDED_SIZE + nub_padding }
                },
                Nub = new Nub
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Margin = new MarginPadding { Right = nub_padding },
                },
            };

            Nub.Current.BindTo(Current);

            Current.DisabledChanged += disabled => labelText.Alpha = Nub.Alpha = disabled ? 0.3f : 1;
        }

        protected override bool OnHover(HoverEvent e)
        {
            Nub.Glowing = true;
            Nub.Expanded = true;
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            Nub.Glowing = false;
            Nub.Expanded = false;
            base.OnHoverLost(e);
        }
    }
}
