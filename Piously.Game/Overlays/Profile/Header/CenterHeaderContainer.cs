using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using Piously.Game.Overlays.Profile.Header.Components;
using Piously.Game.Users;
using osuTK;

namespace Piously.Game.Overlays.Profile.Header
{
    public class CenterHeaderContainer : CompositeDrawable
    {
        public readonly BindableBool DetailsVisible = new BindableBool(true);
        public readonly Bindable<User> User = new Bindable<User>();

        private OverlinedInfoContainer hiddenDetailGlobal;
        private OverlinedInfoContainer hiddenDetailCountry;

        public CenterHeaderContainer()
        {
            Height = 60;
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColorProvider colorProvider, TextureStore textures)
        {
            Container<Drawable> hiddenDetailContainer;
            Container<Drawable> expandedDetailContainer;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colorProvider.Background4
                },
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.X,
                    RelativeSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Padding = new MarginPadding { Vertical = 10 },
                    Margin = new MarginPadding { Left = UserProfileOverlay.CONTENT_X_MARGIN },
                    Spacing = new Vector2(10, 0),
                    Children = new Drawable[]
                    {
                        new AddFriendButton
                        {
                            RelativeSizeAxes = Axes.Y,
                            User = { BindTarget = User }
                        },
                        new MessageUserButton
                        {
                            User = { BindTarget = User }
                        },
                    }
                },
                new Container
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Y,
                    Padding = new MarginPadding { Vertical = 10 },
                    Width = UserProfileOverlay.CONTENT_X_MARGIN,
                    Child = new ExpandDetailsButton
                    {
                        RelativeSizeAxes = Axes.Y,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        DetailsVisible = { BindTarget = DetailsVisible }
                    },
                },
                new Container
                {
                    //TO BE IMPLEMENTED
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    AutoSizeAxes = Axes.Both,
                    Margin = new MarginPadding { Right = UserProfileOverlay.CONTENT_X_MARGIN },
                    Children = new Drawable[]
                    {
                        /*new LevelBadge
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Size = new Vector2(40),
                            User = { BindTarget = User }
                        },*/
                        expandedDetailContainer = new Container
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Width = 200,
                            Height = 6,
                            Margin = new MarginPadding { Right = 50 },
                            /*Child = new LevelProgressBar
                            {
                                RelativeSizeAxes = Axes.Both,
                                User = { BindTarget = User }
                            }*/
                        },
                        hiddenDetailContainer = new FillFlowContainer
                        {
                            Direction = FillDirection.Horizontal,
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Width = 200,
                            AutoSizeAxes = Axes.Y,
                            Alpha = 0,
                            Spacing = new Vector2(10, 0),
                            Margin = new MarginPadding { Right = 50 },
                            Children = new[]
                            {
                                hiddenDetailGlobal = new OverlinedInfoContainer
                                {
                                    Title = "Global Ranking",
                                    LineColour = colorProvider.Highlight1
                                },
                                hiddenDetailCountry = new OverlinedInfoContainer
                                {
                                    Title = "Country Ranking",
                                    LineColour = colorProvider.Highlight1
                                },
                            }
                        }
                    }
                }
            };

            DetailsVisible.BindValueChanged(visible =>
            {
                hiddenDetailContainer.FadeTo(visible.NewValue ? 0 : 1, 200, Easing.OutQuint);
                expandedDetailContainer.FadeTo(visible.NewValue ? 1 : 0, 200, Easing.OutQuint);
            });

            User.BindValueChanged(user => updateDisplay(user.NewValue));
        }

        //TO BE IMPLEMENTED
        private void updateDisplay(User user)
        {
            //hiddenDetailGlobal.Content = user?.Statistics?.Ranks.Global?.ToString("\\##,##0") ?? "-";
            //hiddenDetailCountry.Content = user?.Statistics?.Ranks.Country?.ToString("\\##,##0") ?? "-";
        }
    }
}
