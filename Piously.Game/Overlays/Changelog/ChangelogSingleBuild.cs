using System;
using System.Linq;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Online.API;
using Piously.Game.Online.API.Requests;
using Piously.Game.Online.API.Requests.Responses;
using Piously.Game.Overlays.Comments;
using osuTK;

namespace Piously.Game.Overlays.Changelog
{
    public class ChangelogSingleBuild : ChangelogContent
    {
        private APIChangelogBuild build;

        public ChangelogSingleBuild(APIChangelogBuild build)
        {
            this.build = build;
        }

        [BackgroundDependencyLoader]
        private void load(CancellationToken? cancellation, IAPIProvider api, OverlayColorProvider colorProvider)
        {
            bool complete = false;

            var req = new GetChangelogBuildRequest(build.UpdateStream.Name, build.Version);
            req.Success += res =>
            {
                build = res;
                complete = true;
            };
            req.Failure += _ => complete = true;

            api.PerformAsync(req);

            while (!complete)
            {
                if (cancellation?.IsCancellationRequested == true)
                {
                    req.Cancel();
                    return;
                }

                Thread.Sleep(10);
            }

            if (build != null)
            {
                CommentsContainer comments;

                Children = new Drawable[]
                {
                    new ChangelogBuildWithNavigation(build) { SelectBuild = SelectBuild },
                    new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 2,
                        Colour = colorProvider.Background6,
                        Margin = new MarginPadding { Top = 30 },
                    },
                    comments = new CommentsContainer()
                };

                comments.ShowComments(CommentableType.Build, build.Id);
            }
        }

        public class ChangelogBuildWithNavigation : ChangelogBuild
        {
            public ChangelogBuildWithNavigation(APIChangelogBuild build)
                : base(build)
            {
            }

            private PiouslySpriteText date;

            protected override FillFlowContainer CreateHeader()
            {
                var fill = base.CreateHeader();

                foreach (var existing in fill.Children.OfType<PiouslyHoverContainer>())
                {
                    existing.Scale = new Vector2(1.25f);
                    existing.Action = null;

                    existing.Add(date = new PiouslySpriteText
                    {
                        Text = Build.CreatedAt.Date.ToString("dd MMMM yyyy"),
                        Font = PiouslyFont.GetFont(weight: FontWeight.Regular, size: 14),
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.TopCentre,
                        Margin = new MarginPadding { Top = 5 },
                    });
                }

                fill.Insert(-1, new NavigationIconButton(Build.Versions?.Previous)
                {
                    Icon = FontAwesome.Solid.ChevronLeft,
                    SelectBuild = b => SelectBuild(b)
                });
                fill.Insert(1, new NavigationIconButton(Build.Versions?.Next)
                {
                    Icon = FontAwesome.Solid.ChevronRight,
                    SelectBuild = b => SelectBuild(b)
                });

                return fill;
            }

            [BackgroundDependencyLoader]
            private void load(OverlayColorProvider colorProvider)
            {
                date.Colour = colorProvider.Light1;
            }
        }

        private class NavigationIconButton : IconButton
        {
            public Action<APIChangelogBuild> SelectBuild;

            public NavigationIconButton(APIChangelogBuild build)
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                if (build == null) return;

                TooltipText = build.DisplayVersion;

                Action = () =>
                {
                    SelectBuild?.Invoke(build);
                    Enabled.Value = false;
                };
            }

            [BackgroundDependencyLoader]
            private void load(PiouslyColor colors)
            {
                HoverColour = colors.GreyVioletLight.Opacity(0.6f);
                FlashColour = colors.GreyVioletLighter;
            }
        }
    }
}
