using System;
using System.Globalization;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslySliderBar<T> : SliderBar<T>, IHasTooltip, IHasAccentColour
        where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
        /// <summary>
        /// Maximum number of decimal digits to be displayed in the tooltip.
        /// </summary>
        private const int max_decimal_digits = 5;

        protected readonly Nub Nub;
        private readonly Box leftBox;
        private readonly Box rightBox;
        private readonly Container nubContainer;

        public virtual LocalisableString TooltipText { get; private set; }

        /// <summary>
        /// Whether to format the tooltip as a percentage or the actual value.
        /// </summary>
        public bool DisplayAsPercentage { get; set; }

        private Color4 accentColour;

        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                accentColour = value;
                leftBox.Colour = value;
                rightBox.Colour = value;
            }
        }

        public PiouslySliderBar()
        {
            Height = 12;
            RangePadding = 20;
            Children = new Drawable[]
            {
                leftBox = new Box
                {
                    Height = 2,
                    EdgeSmoothness = new Vector2(0, 0.5f),
                    Position = new Vector2(2, 0),
                    RelativeSizeAxes = Axes.None,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                },
                rightBox = new Box
                {
                    Height = 2,
                    EdgeSmoothness = new Vector2(0, 0.5f),
                    Position = new Vector2(-2, 0),
                    RelativeSizeAxes = Axes.None,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Alpha = 0.5f,
                },
                nubContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = Nub = new Nub
                    {
                        Origin = Anchor.TopCentre,
                        RelativePositionAxes = Axes.X,
                        Expanded = true,
                    },
                },
            };

            Current.DisabledChanged += disabled => { Alpha = disabled ? 0.3f : 1; };
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colours)
        {
            AccentColour = colours.Pink;
        }

        protected override void Update()
        {
            base.Update();

            nubContainer.Padding = new MarginPadding { Horizontal = RangePadding };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            CurrentNumber.BindValueChanged(current => updateTooltipText(current.NewValue), true);
        }

        protected override bool OnHover(HoverEvent e)
        {
            Nub.Glowing = true;
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            Nub.Glowing = false;
            base.OnHoverLost(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            Nub.Current.Value = true;
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            Nub.Current.Value = false;
            base.OnMouseUp(e);
        }

        protected override void OnUserChange(T value)
        {
            base.OnUserChange(value);
            updateTooltipText(value);
        }

        private void updateTooltipText(T value)
        {
            if (CurrentNumber.IsInteger)
                TooltipText = value.ToInt32(NumberFormatInfo.InvariantInfo).ToString("N0");
            else
            {
                double floatValue = value.ToDouble(NumberFormatInfo.InvariantInfo);

                if (DisplayAsPercentage)
                {
                    TooltipText = floatValue.ToString("0%");
                }
                else
                {
                    var decimalPrecision = normalise(CurrentNumber.Precision.ToDecimal(NumberFormatInfo.InvariantInfo), max_decimal_digits);

                    // Find the number of significant digits (we could have less than 5 after normalize())
                    var significantDigits = findPrecision(decimalPrecision);

                    TooltipText = floatValue.ToString($"N{significantDigits}");
                }
            }
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();
            leftBox.Scale = new Vector2(Math.Clamp(
                RangePadding + Nub.DrawPosition.X - Nub.DrawWidth / 2, 0, DrawWidth), 1);
            rightBox.Scale = new Vector2(Math.Clamp(
                DrawWidth - Nub.DrawPosition.X - RangePadding - Nub.DrawWidth / 2, 0, DrawWidth), 1);
        }

        protected override void UpdateValue(float value)
        {
            Nub.MoveToX(value, 250, Easing.OutQuint);
        }

        /// <summary>
        /// Removes all non-significant digits, keeping at most a requested number of decimal digits.
        /// </summary>
        /// <param name="d">The decimal to normalize.</param>
        /// <param name="sd">The maximum number of decimal digits to keep. The final result may have fewer decimal digits than this value.</param>
        /// <returns>The normalised decimal.</returns>
        private decimal normalise(decimal d, int sd)
            => decimal.Parse(Math.Round(d, sd).ToString(string.Concat("0.", new string('#', sd)), CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        /// <summary>
        /// Finds the number of digits after the decimal.
        /// </summary>
        /// <param name="d">The value to find the number of decimal digits for.</param>
        /// <returns>The number decimal digits.</returns>
        private int findPrecision(decimal d)
        {
            int precision = 0;

            while (d != Math.Round(d))
            {
                d *= 10;
                precision++;
            }

            return precision;
        }
    }
}
