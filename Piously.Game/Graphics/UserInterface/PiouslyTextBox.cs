using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using Piously.Game.Graphics.Sprites;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using Piously.Game.Graphics.Containers;
using osuTK;

namespace Piously.Game.Graphics.UserInterface
{
    public class PiouslyTextBox : BasicTextBox
    {
        private readonly SampleChannel[] textAddedSamples = new SampleChannel[4];
        private SampleChannel capsTextAddedSample;
        private SampleChannel textRemovedSample;
        private SampleChannel textCommittedSample;
        private SampleChannel caretMovedSample;

        /// <summary>
        /// Whether to allow playing a different samples based on the type of character.
        /// If set to false, the same sample will be used for all characters.
        /// </summary>
        protected virtual bool AllowUniqueCharacterSamples => true;

        protected override float LeftRightPadding => 10;

        protected override float CaretWidth => 3;

        protected override SpriteText CreatePlaceholder() => new PiouslySpriteText
        {
            Font = PiouslyFont.GetFont(italics: true),
            Colour = new Color4(180, 180, 180, 255),
            Margin = new MarginPadding { Left = 2 },
        };

        public PiouslyTextBox()
        {
            Height = 40;
            TextContainer.Height = 0.5f;
            CornerRadius = 5;
            LengthLimit = 1000;

            Current.DisabledChanged += disabled => { Alpha = disabled ? 0.3f : 1; };
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor color, AudioManager audio)
        {
            BackgroundUnfocused = Color4.Black.Opacity(0.5f);
            BackgroundFocused = PiouslyColor.Gray(0.3f).Opacity(0.8f);
            BackgroundCommit = BorderColour = color.Yellow;

            for (int i = 0; i < textAddedSamples.Length; i++)
                textAddedSamples[i] = audio.Samples.Get($@"Keyboard/key-press-{1 + i}");

            capsTextAddedSample = audio.Samples.Get(@"Keyboard/key-caps");
            textRemovedSample = audio.Samples.Get(@"Keyboard/key-delete");
            textCommittedSample = audio.Samples.Get(@"Keyboard/key-confirm");
            caretMovedSample = audio.Samples.Get(@"Keyboard/key-movement");
        }

        protected override Color4 SelectionColour => new Color4(249, 90, 255, 255);

        protected override void OnUserTextAdded(string added)
        {
            base.OnUserTextAdded(added);

            if (added.Any(char.IsUpper) && AllowUniqueCharacterSamples)
                capsTextAddedSample?.Play();
            else
                textAddedSamples[RNG.Next(0, 3)]?.Play();
        }

        protected override void OnUserTextRemoved(string removed)
        {
            base.OnUserTextRemoved(removed);

            textRemovedSample?.Play();
        }

        protected override void OnTextCommitted(bool textChanged)
        {
            base.OnTextCommitted(textChanged);

            textCommittedSample?.Play();
        }

        protected override void OnCaretMoved(bool selecting)
        {
            base.OnCaretMoved(selecting);

            caretMovedSample?.Play();
        }

        protected override void OnFocus(FocusEvent e)
        {
            BorderThickness = 3;
            base.OnFocus(e);
        }

        protected override void OnFocusLost(FocusLostEvent e)
        {
            BorderThickness = 0;

            base.OnFocusLost(e);
        }

        protected override Drawable GetDrawableCharacter(char c) => new FallingDownContainer
        {
            AutoSizeAxes = Axes.Both,
            Child = new PiouslySpriteText { Text = c.ToString(), Font = PiouslyFont.GetFont(size: CalculatedTextSize) },
        };

        protected override Caret CreateCaret() => new PiouslyCaret
        {
            CaretWidth = CaretWidth,
            SelectionColour = SelectionColour,
        };

        private class PiouslyCaret : Caret
        {
            private const float caret_move_time = 60;

            private readonly Container beatSync;

            public PiouslyCaret()
            {
                RelativeSizeAxes = Axes.Y;
                Size = new Vector2(1, 0.9f);

                Colour = Color4.Transparent;
                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                Masking = true;
                CornerRadius = 1;
                InternalChild = beatSync = new PiouslyHoverContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White,
                };
            }

            public override void Hide() => this.FadeOut(200);

            public float CaretWidth { get; set; }

            public Color4 SelectionColour { get; set; }

            //TO BE IMPLEMENTED
            public override void DisplayAt(Vector2 position, float? selectionWidth)
            {
                if (selectionWidth != null)
                {
                    this.MoveTo(new Vector2(position.X, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(selectionWidth.Value + CaretWidth / 2, caret_move_time, Easing.Out);
                    this.FadeColour(SelectionColour, 200, Easing.Out);
                }
                else
                {
                    this.MoveTo(new Vector2(position.X - CaretWidth / 2, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(CaretWidth, caret_move_time, Easing.Out);
                    this.FadeColour(Color4.White, 200, Easing.Out);
                }
            }
        }
    }
}
