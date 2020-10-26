using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Graphics.Containers;

namespace Piously.Game.Overlays.Settings
{
    public class SidebarButton : PiouslyButton
    {
        private readonly ConstrainedIconContainer iconContainer;
        private readonly SpriteText headerText;
        private readonly Box selectionIndicator;
        private readonly Container text;

        private SettingsSection section;

        public SettingsSection Section
        {
            get => section;
            set
            {
                section = value;
                headerText.Text = value.Header;
                iconContainer.Icon = value.CreateIcon();
            }
        }

        private bool selected;

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;

                if (selected)
                {
                    selectionIndicator.FadeIn(50);
                    text.FadeColour(Color4.White, 50);
                }
                else
                {
                    selectionIndicator.FadeOut(50);
                    text.FadeColour(PiouslyColor.Gray(0.6f), 50);
                }
            }
        }

        public SidebarButton()
        {
            Height = Sidebar.DEFAULT_WIDTH;
            RelativeSizeAxes = Axes.X;

            BackgroundColour = Color4.Black;

            AddRange(new Drawable[]
            {
                text = new Container
                {
                    Width = Sidebar.DEFAULT_WIDTH,
                    RelativeSizeAxes = Axes.Y,
                    Colour = PiouslyColor.Gray(0.6f),
                    Children = new Drawable[]
                    {
                        headerText = new PiouslySpriteText
                        {
                            Position = new Vector2(Sidebar.DEFAULT_WIDTH + 10, 0),
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                        },
                        iconContainer = new ConstrainedIconContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(20),
                        },
                    }
                },
                selectionIndicator = new Box
                {
                    Alpha = 0,
                    RelativeSizeAxes = Axes.Y,
                    Width = 5,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                },
            });
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyColor colors)
        {
            selectionIndicator.Colour = colors.Yellow;
        }
    }
}
