using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Overlays;
using osu.Framework.Graphics.UserInterface;
using Piously.Game.Graphics.UserInterface;
using osu.Framework.Graphics.Cursor;
using Piously.Game.Graphics.Containers;
using JetBrains.Annotations;

namespace Piously.Game.Users
{
    public abstract class UserPanel : PiouslyClickableContainer, IHasContextMenu
    {
        public readonly User User;

        public new Action Action;

        protected Action ViewProfile { get; private set; }

        protected Drawable Background { get; private set; }

        protected UserPanel(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            User = user;
        }

        [Resolved(canBeNull: true)]
        private UserProfileOverlay profileOverlay { get; set; }

        [Resolved(canBeNull: true)]
        protected OverlayColorProvider ColorProvider { get; private set; }

        [Resolved]
        protected PiouslyColor Colors { get; private set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;

            AddRange(new[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ColorProvider?.Background5 ?? Colors.Gray1
                },
                Background = new UserCoverBackground
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    User = User,
                },
                CreateLayout()
            });

            base.Action = ViewProfile = () =>
            {
                Action?.Invoke();
                profileOverlay?.ShowUser(User);
            };
        }

        [NotNull]
        protected abstract Drawable CreateLayout();

        protected PiouslySpriteText CreateUsername() => new PiouslySpriteText
        {
            Font = PiouslyFont.GetFont(size: 16, weight: FontWeight.Bold),
            Shadow = false,
            Text = User.Username,
        };

        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new PiouslyMenuItem("View Profile", MenuItemType.Highlighted, ViewProfile),
        };
    }
}
