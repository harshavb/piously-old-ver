using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Users;
using Piously.Game.Users.Drawables;
using osuTK;

namespace Piously.Game.Overlays.Profile.Header
{
    public class TopHeaderContainer : CompositeDrawable
    {
        private const float avatar_size = 110;

        public readonly Bindable<User> User = new Bindable<User>();

        private UpdateableAvatar avatar;
        private PiouslySpriteText usernameText;
        private ExternalLinkButton openUserExternally;
        private PiouslySpriteText titleText;
        private UpdateableFlag userFlag;
        private PiouslySpriteText userCountryText;
        private FillFlowContainer userStats;

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider)
        {
            Height = 150;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colorProvider.Background5,
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    Margin = new MarginPadding { Left = UserProfileOverlay.CONTENT_X_MARGIN },
                    Height = avatar_size,
                    AutoSizeAxes = Axes.X,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Children = new Drawable[]
                    {
                        avatar = new UpdateableAvatar
                        {
                            Size = new Vector2(avatar_size),
                            Masking = true,
                            CornerRadius = avatar_size * 0.25f,
                            OpenOnClick = { Value = false },
                            ShowGuestOnNull = false,
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.Y,
                            AutoSizeAxes = Axes.X,
                            Padding = new MarginPadding { Left = 10 },
                            Children = new Drawable[]
                            {
                                new FillFlowContainer
                                {
                                    AutoSizeAxes = Axes.Both,
                                    Direction = FillDirection.Vertical,
                                    Children = new Drawable[]
                                    {
                                        new FillFlowContainer
                                        {
                                            AutoSizeAxes = Axes.Both,
                                            Direction = FillDirection.Horizontal,
                                            Children = new Drawable[]
                                            {
                                                usernameText = new PiouslySpriteText
                                                {
                                                    Font = PiouslyFont.GetFont(size: 24, weight: FontWeight.Regular)
                                                },
                                                openUserExternally = new ExternalLinkButton
                                                {
                                                    Margin = new MarginPadding { Left = 5 },
                                                    Anchor = Anchor.CentreLeft,
                                                    Origin = Anchor.CentreLeft,
                                                },
                                            }
                                        },
                                        titleText = new PiouslySpriteText
                                        {
                                            Font = PiouslyFont.GetFont(size: 18, weight: FontWeight.Regular)
                                        },
                                    }
                                },
                                new FillFlowContainer
                                {
                                    Origin = Anchor.BottomLeft,
                                    Anchor = Anchor.BottomLeft,
                                    Direction = FillDirection.Vertical,
                                    AutoSizeAxes = Axes.Both,
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.X,
                                            Height = 1.5f,
                                            Margin = new MarginPadding { Top = 10 },
                                            Colour = colorProvider.Light1,
                                        },
                                        new FillFlowContainer
                                        {
                                            AutoSizeAxes = Axes.Both,
                                            Margin = new MarginPadding { Top = 5 },
                                            Direction = FillDirection.Horizontal,
                                            Children = new Drawable[]
                                            {
                                                userFlag = new UpdateableFlag
                                                {
                                                    Size = new Vector2(30, 20),
                                                    ShowPlaceholderOnNull = false,
                                                },
                                                userCountryText = new PiouslySpriteText
                                                {
                                                    Font = PiouslyFont.GetFont(size: 17.5f, weight: FontWeight.Regular),
                                                    Margin = new MarginPadding { Left = 10 },
                                                    Origin = Anchor.CentreLeft,
                                                    Anchor = Anchor.CentreLeft,
                                                    Colour = colorProvider.Light1,
                                                }
                                            }
                                        },
                                    }
                                }
                            }
                        }
                    }
                },
                userStats = new FillFlowContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    AutoSizeAxes = Axes.Y,
                    Width = 300,
                    Margin = new MarginPadding { Right = UserProfileOverlay.CONTENT_X_MARGIN },
                    Padding = new MarginPadding { Vertical = 15 },
                    Spacing = new Vector2(0, 2)
                }
            };

            User.BindValueChanged(user => updateUser(user.NewValue));
        }

        private void updateUser(User user)
        {
            avatar.User = user;
            usernameText.Text = user?.Username ?? string.Empty;
            openUserExternally.Link = $@"https://osu.ppy.sh/users/{user?.Id ?? 0}";
            userFlag.Country = user?.Country;
            userCountryText.Text = user?.Country?.FullName ?? "Alien";
            titleText.Text = user?.Title ?? string.Empty;
            titleText.Colour = Color4Extensions.FromHex(user?.Color ?? "fff");

            userStats.Clear();

            //TO BE IMPLEMENTED
            if (user?.Statistics != null)
            {
                //userStats.Add(new UserStatsLine("Ranked Score", user.Statistics.RankedScore.ToString("#,##0")));
                //userStats.Add(new UserStatsLine("Hit Accuracy", user.Statistics.DisplayAccuracy));
                //userStats.Add(new UserStatsLine("Play Count", user.Statistics.PlayCount.ToString("#,##0")));
                //userStats.Add(new UserStatsLine("Total Score", user.Statistics.TotalScore.ToString("#,##0")));
                //userStats.Add(new UserStatsLine("Total Hits", user.Statistics.TotalHits.ToString("#,##0")));
                //userStats.Add(new UserStatsLine("Maximum Combo", user.Statistics.MaxCombo.ToString("#,##0")));
                //userStats.Add(new UserStatsLine("Replays Watched by Others", user.Statistics.ReplaysWatched.ToString("#,##0")));
            }
        }

        private class UserStatsLine : Container
        {
            public UserStatsLine(string left, string right)
            {
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;
                Children = new Drawable[]
                {
                    new PiouslySpriteText
                    {
                        Font = PiouslyFont.GetFont(size: 15),
                        Text = left,
                    },
                    new PiouslySpriteText
                    {
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        Font = PiouslyFont.GetFont(size: 15, weight: FontWeight.Bold),
                        Text = right,
                    },
                };
            }
        }
    }
}
