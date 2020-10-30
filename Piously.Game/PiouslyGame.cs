using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using Piously.Game.Graphics.Containers;
using Piously.Game.Input.Bindings;
using osuTK;
using osuTK.Graphics;
using LogLevel = osu.Framework.Logging.LogLevel;
using osuTK.Graphics.ES20;
using osu.Framework.Graphics.Containers;

namespace Piously.Game
{
    //The actual game, specifically loads the UI
    public class PiouslyGame : PiouslyGameBase, IKeyBindingHandler<GlobalAction>
    {
        private Box box;

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance)
            {
                Logger.Log(@"Piously does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            Add(new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Margin = new MarginPadding { Top = 20 },
                Children = new Drawable[]
                {
                    box = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(150, 150),
                        Colour = Color4.Tomato
                    },
                    new TestClickableContainer
                    {
                        Child = new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(150, 150),
                            Colour = Color4.Tomato
                        },
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Both,
                    },
                    new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = "Testing font",
                        Font = new FontUsage(family: "InkFree-Bold", size: 40f)
                    }
                }
            });
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.TestAction1:
                    box.Colour = Color4.GreenYellow;
                    break;
                case GlobalAction.TestAction2:
                    box.Colour = Color4.HotPink;
                    break;
            }
            return true;
        }

        public void OnReleased(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.TestAction1:
                    box.Colour = Color4.Tomato;
                    break;
                case GlobalAction.TestAction2:
                    box.Colour = Color4.Tomato;
                    break;
            }
        }

        protected override void Update()
        {
            base.Update();
            box.Rotation += (float)Time.Elapsed / 10;
        }
    }
}