using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.Cursor;
using Piously.Game.Utils;

namespace Piously.Game.Graphics
{
    public class DrawableDate : SpriteText, IHasCustomTooltip
    {
        private DateTimeOffset date;

        public DateTimeOffset Date
        {
            get => date;
            set
            {
                if (date == value)
                    return;

                date = value.ToLocalTime();

                if (LoadState >= LoadState.Ready)
                    updateTime();
            }
        }

        public DrawableDate(DateTimeOffset date, float textSize = 16)
        {
            Font = new FontUsage(size: textSize);
            Date = date;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            updateTime();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Scheduler.Add(updateTimeWithReschedule);
        }

        private void updateTimeWithReschedule()
        {
            updateTime();

            var diffToNow = DateTimeOffset.Now.Subtract(Date);

            double timeUntilNextUpdate = 1000;

            if (Math.Abs(diffToNow.TotalSeconds) > 120)
            {
                timeUntilNextUpdate *= 60;

                if (Math.Abs(diffToNow.TotalMinutes) > 120)
                {
                    timeUntilNextUpdate *= 60;

                    if (Math.Abs(diffToNow.TotalHours) > 48)
                        timeUntilNextUpdate *= 24;
                }
            }

            Scheduler.AddDelayed(updateTimeWithReschedule, timeUntilNextUpdate);
        }

        protected virtual string Format() => HumanizerUtils.Humanize(Date);

        private void updateTime() => Text = Format();

        public ITooltip GetCustomTooltip() => new DateTooltip();

        public object TooltipContent => Date;
    }
}
