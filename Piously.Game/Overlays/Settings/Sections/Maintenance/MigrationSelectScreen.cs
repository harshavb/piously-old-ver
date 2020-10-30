using System;
using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu.Framework.Screens;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Graphics.UserInterfaceV2;
using Piously.Game.Screens;
using osuTK;

namespace Piously.Game.Overlays.Settings.Sections.Maintenance
{
    public class MigrationSelectScreen : PiouslyScreen
    {
        private DirectorySelector directorySelector;

        public override bool AllowExternalScreenChange => false;

        public override bool HideOverlaysOnEnter => true;

        [BackgroundDependencyLoader(true)]
        private void load(PiouslyGame game, Storage storage, PiouslyColor colors)
        {
            game?.Toolbar.Hide();

            // begin selection in the parent directory of the current storage location
            var initialPath = new DirectoryInfo(storage.GetFullPath(string.Empty)).Parent?.FullName;

            InternalChild = new Container
            {
                Masking = true,
                CornerRadius = 10,
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(0.5f, 0.8f),
                Children = new Drawable[]
                {
                    new Box
                    {
                        Colour = colors.GreySeafoamDark,
                        RelativeSizeAxes = Axes.Both,
                    },
                    new GridContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        RowDimensions = new[]
                        {
                            new Dimension(),
                            new Dimension(GridSizeMode.Relative, 0.8f),
                            new Dimension(),
                        },
                        Content = new[]
                        {
                            new Drawable[]
                            {
                                new PiouslySpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Text = "Please select a new location",
                                    Font = PiouslyFont.Default.With(size: 40)
                                },
                            },
                            new Drawable[]
                            {
                                directorySelector = new DirectorySelector(initialPath)
                                {
                                    RelativeSizeAxes = Axes.Both,
                                }
                            },
                            new Drawable[]
                            {
                                new TriangleButton
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Width = 300,
                                    Text = "Begin folder migration",
                                    Action = start
                                },
                            }
                        }
                    }
                }
            };
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);

            this.FadeOut(250);
        }

        private void start()
        {
            var target = directorySelector.CurrentPath.Value;

            try
            {
                if (target.GetDirectories().Length > 0 || target.GetFiles().Length > 0)
                    target = target.CreateSubdirectory("osu-lazer");
            }
            catch (Exception e)
            {
                Logger.Log($"Error during migration: {e.Message}", level: LogLevel.Error);
                return;
            }

            ValidForResume = false;
            BeginMigration(target);
        }

        protected virtual void BeginMigration(DirectoryInfo target) => this.Push(new MigrationRunScreen(target));
    }
}
