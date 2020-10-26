using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Utils;
using osu.Framework.Threading;
using Piously.Game.Configuration;
using Piously.Game.Screens;
using Piously.Game.Online.API;
using Piously.Game.Users;

namespace Piously.Game.Screens.Backgrounds
{
    public class BackgroundScreenDefault : BackgroundScreen
    {
        private Background background;

        private int currentDisplay;
        private const int background_count = 7;
        private Bindable<User> user;
        private Bindable<IntroSequence> introSequence;

        public BackgroundScreenDefault(bool animateOnEnter = true)
            : base(animateOnEnter)
        {
        }

        [BackgroundDependencyLoader]
        private void load(IAPIProvider api, PiouslyConfigManager config)
        {
            user = api.LocalUser.GetBoundCopy();
            introSequence = config.GetBindable<IntroSequence>(PiouslySetting.IntroSequence);

            user.ValueChanged += _ => Next();
            introSequence.ValueChanged += _ => Next();

            currentDisplay = RNG.Next(0, background_count);

            display(createBackground());
        }

        private void display(Background newBackground)
        {
            background?.FadeOut(800, Easing.InOutSine);
            background?.Expire();

            AddInternal(background = newBackground);
            currentDisplay++;
        }

        private ScheduledDelegate nextTask;

        public void Next()
        {
            nextTask?.Cancel();
            nextTask = Scheduler.AddDelayed(() => { LoadComponentAsync(createBackground(), display); }, 100);
        }

        private Background createBackground()
        {
            Background newBackground;
            string backgroundName;

            switch (introSequence.Value)
            {
                case IntroSequence.Welcome:
                    backgroundName = "Intro/Welcome/menu-background";
                    break;

                default:
                    backgroundName = $@"Menu/menu-background-{currentDisplay % background_count + 1}";
                    break;
            }

            newBackground = new Background(backgroundName);

            newBackground.Depth = currentDisplay;

            return newBackground;
        }
    }
}
