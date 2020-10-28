using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics;
using osuTK;

namespace Piously.Game.Users
{
    public class UserBrickPanel : UserPanel
    {
        public UserBrickPanel(User user)
            : base(user)
        {
            AutoSizeAxes = Axes.Both;
            CornerRadius = 6;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Background.FadeTo(0.2f);
        }

        protected override Drawable CreateLayout() => new FillFlowContainer
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Horizontal,
            Spacing = new Vector2(5, 0),
            Margin = new MarginPadding
            {
                Horizontal = 10,
                Vertical = 3,
            },
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Children = new Drawable[]
            {
                new CircularContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Masking = true,
                    Width = 4,
                    Height = 13,
                    Child = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = string.IsNullOrEmpty(User.Color) ? Color4Extensions.FromHex("0087ca") : Color4Extensions.FromHex(User.Color)
                    }
                },
                CreateUsername().With(u =>
                {
                    u.Anchor = Anchor.CentreLeft;
                    u.Origin = Anchor.CentreLeft;
                    u.Font = PiouslyFont.GetFont(size: 13, weight: FontWeight.Bold);
                })
            }
        };
    }
}
