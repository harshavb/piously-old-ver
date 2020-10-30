using System.IO;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Logging;
using osu.Framework.Screens;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Screens;
using osuTK;

namespace Piously.Game.Overlays.Settings.Sections.Maintenance
{
    public class MigrationRunScreen : PiouslyScreen
    {
        private readonly DirectoryInfo destination;

        [Resolved(canBeNull: true)]
        private PiouslyGame game { get; set; }

        public override bool AllowBackButton => false;

        public override bool AllowExternalScreenChange => false;

        public override bool HideOverlaysOnEnter => true;

        private Task migrationTask;

        public MigrationRunScreen(DirectoryInfo destination)
        {
            this.destination = destination;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Spacing = new Vector2(10),
                    Children = new Drawable[]
                    {
                        new PiouslySpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "Migration in progress",
                            Font = PiouslyFont.Default.With(size: 40)
                        },
                        new PiouslySpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "This could take a few minutes depending on the speed of your disk(s).",
                            Font = PiouslyFont.Default.With(size: 30)
                        },
                        new LoadingSpinner(true)
                        {
                            State = { Value = Visibility.Visible }
                        },
                        new PiouslySpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "Please avoid interacting with the game!",
                            Font = PiouslyFont.Default.With(size: 30)
                        },
                    }
                },
            };

            migrationTask = Task.Run(PerformMigration)
                                .ContinueWith(t =>
                                {
                                    if (t.IsFaulted)
                                        Logger.Log($"Error during migration: {t.Exception?.Message}", level: LogLevel.Error);

                                    Schedule(this.Exit);
                                });
        }

        protected virtual void PerformMigration() => game?.Migrate(destination.FullName);

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            this.FadeOut().Delay(250).Then().FadeIn(250);
        }

        public override bool OnExiting(IScreen next)
        {
            // block until migration is finished
            if (migrationTask?.IsCompleted == false)
                return true;

            return base.OnExiting(next);
        }
    }
}
