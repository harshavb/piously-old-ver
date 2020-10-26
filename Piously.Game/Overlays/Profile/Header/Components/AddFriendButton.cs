using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Users;
using osuTK;

namespace Piously.Game.Overlays.Profile.Header.Components
{
    public class AddFriendButton : ProfileHeaderButton
    {
        public readonly Bindable<User> User = new Bindable<User>();

        public override string TooltipText => "friends";

        private PiouslySpriteText followerText;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Direction = FillDirection.Horizontal,
                Padding = new MarginPadding { Right = 10 },
                Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Icon = FontAwesome.Solid.User,
                        FillMode = FillMode.Fit,
                        Size = new Vector2(50, 14)
                    },
                    followerText = new PiouslySpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Font = PiouslyFont.GetFont(weight: FontWeight.Bold)
                    }
                }
            };

            //TO BE IMPLEMENTED
            //User.BindValueChanged(user => updateFollowers(user.NewValue), true);
        }

        //TO BE IMPLEMENTED
        //private void updateFollowers(User user) => followerText.Text = user?.FollowerCount.ToString("#,##0");
    }
}
