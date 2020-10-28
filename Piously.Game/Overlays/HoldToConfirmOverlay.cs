using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics.Containers;
using osuTK.Graphics;

namespace Piously.Game.Overlays
{
    /// <summary>
    /// An overlay which will display a black screen that dims over a period before confirming an exit action.
    /// Action is BYO (derived class will need to call <see cref="HoldToConfirmContainer.BeginConfirm"/> and <see cref="HoldToConfirmContainer.AbortConfirm"/> from a user event).
    /// </summary>
    public abstract class HoldToConfirmOverlay : HoldToConfirmContainer
    {
        private Box overlay;

        private readonly BindableDouble audioVolume = new BindableDouble(1);

        [Resolved]
        private AudioManager audio { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            AlwaysPresent = true;

            Children = new Drawable[]
            {
                overlay = new Box
                {
                    Alpha = 0,
                    Colour = Color4.Black,
                    RelativeSizeAxes = Axes.Both,
                }
            };

            Progress.ValueChanged += p =>
            {
                audioVolume.Value = 1 - p.NewValue;
                overlay.Alpha = (float)p.NewValue;
            };

            audio.Tracks.AddAdjustment(AdjustableProperty.Volume, audioVolume);
        }

        protected override void Dispose(bool isDisposing)
        {
            audio?.Tracks.RemoveAdjustment(AdjustableProperty.Volume, audioVolume);
            base.Dispose(isDisposing);
        }
    }
}
