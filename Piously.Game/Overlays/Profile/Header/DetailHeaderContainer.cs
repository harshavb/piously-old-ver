using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
//using Piously.Game.Online.Leaderboards;
using Piously.Game.Overlays.Profile.Header.Components;
using Piously.Game.Users;
using osuTK;


namespace Piously.Game.Overlays.Profile.Header
{
    public class DetailHeaderContainer : CompositeDrawable
    {
        //TO BE IMPLEMENTED
        //private OverlinedInfoContainer detailGlobalRank;
        //private OverlinedInfoContainer detailCountryRank;
        private FillFlowContainer fillFlow;
        //private RankGraph rankGraph;

        public readonly Bindable<User> User = new Bindable<User>();

        private bool expanded = true;

        public bool Expanded
        {
            set
            {
                if (expanded == value) return;

                expanded = value;

                if (fillFlow == null) return;

                fillFlow.ClearTransforms();

                if (expanded)
                    fillFlow.AutoSizeAxes = Axes.Y;
                else
                {
                    fillFlow.AutoSizeAxes = Axes.None;
                    fillFlow.ResizeHeightTo(0, 200, Easing.OutQuint);
                }
            }
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider, PiouslyColor colors)
        {
            AutoSizeAxes = Axes.Y;

            User.ValueChanged += e => updateDisplay(e.NewValue);

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colorProvider.Background5,
                },
                fillFlow = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = expanded ? Axes.Y : Axes.None,
                    AutoSizeDuration = 200,
                    AutoSizeEasing = Easing.OutQuint,
                    Masking = true,
                    Padding = new MarginPadding { Horizontal = UserProfileOverlay.CONTENT_X_MARGIN, Vertical = 10 },
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 20),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                new FillFlowContainer
                                {
                                    AutoSizeAxes = Axes.Both,
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Direction = FillDirection.Horizontal,
                                    Spacing = new Vector2(10, 0),
                                    Children = new Drawable[]
                                    {
                                        //TO BE IMPLEMENTED
                                        /*new OverlinedTotalPlayTime
                                        {
                                            User = { BindTarget = User }
                                        }*/
                                    }
                                }
                            }
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding { Right = 130 },
                            Children = new Drawable[]
                            {
                                //TO BE IMPLEMENTED
                                /*rankGraph = new RankGraph
                                {
                                    RelativeSizeAxes = Axes.Both,
                                }*/
                                new FillFlowContainer
                                {
                                    AutoSizeAxes = Axes.Y,
                                    Width = 130,
                                    Anchor = Anchor.TopRight,
                                    Direction = FillDirection.Vertical,
                                    Padding = new MarginPadding { Horizontal = 10 },
                                    Spacing = new Vector2(0, 20),
                                    Children = new Drawable[]
                                    {
                                        //TO BE IMPLEMENTED
                                        /*detailGlobalRank = new OverlinedInfoContainer(true, 110)
                                        {
                                            Title = "Global Ranking",
                                            LineColour = colorProvider.Highlight1,
                                        },
                                        detailCountryRank = new OverlinedInfoContainer(false, 110)
                                        {
                                            Title = "Country Ranking",
                                            LineColour = colourProvider.Highlight1,
                                        },*/
                                    }
                                }
                            }
                        },
                    }
                },
            };
        }

        private void updateDisplay(User user)
        {
            //TO BE IMPLEMENTED
            /*detailGlobalRank.Content = user?.Statistics?.Ranks.Global?.ToString("\\##,##0") ?? "-";
            detailCountryRank.Content = user?.Statistics?.Ranks.Country?.ToString("\\##,##0") ?? "-";

            rankGraph.Statistics.Value = user?.Statistics;*/
        }

        private class ScoreRankInfo : CompositeDrawable
        {
            private readonly PiouslySpriteText rankCount;

            public int RankCount
            {
                set => rankCount.Text = value.ToString("#,##0");
            }

            public ScoreRankInfo(/*ScoreRank rank*/)
            {
                AutoSizeAxes = Axes.Both;
                InternalChild = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    Width = 56,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        //TO BE IMPLEMENTED
                        /*new DrawableRank(rank)
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 30,
                        },
                        rankCount = new PiouslySpriteText
                        {
                            Font = PiouslyFont.GetFont(size: 12, weight: FontWeight.Bold),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre
                        }*/
                    }
                };
            }
        }
    }
}
