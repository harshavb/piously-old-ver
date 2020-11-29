using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Overlays
{
    public abstract class SettingsSubPanel : SettingsPanel
    {
        protected SettingsSubPanel()
            : base()
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new BackButton
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                Action = Hide
            });
        }

        protected override bool DimMainContent => false; // dimming is handled by main overlay

        private class BackButton : PiouslyButton
        {
            [BackgroundDependencyLoader]
            private void load()
            {
                BackgroundColour = Color4.Black;

                AddRange(new Drawable[]
                {
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Children = new Drawable[]
                        {
                            new SpriteIcon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Size = new Vector2(15),
                                Shadow = true,
                                Icon = FontAwesome.Solid.ChevronLeft
                            },
                            new SpriteText
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Y = 15,
                                Font = new FontUsage(size: 12),
                                Text = @"back",
                            },
                        }
                    }
                });
            }
        }
    }
}
