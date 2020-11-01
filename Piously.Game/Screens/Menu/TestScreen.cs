using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Piously.Game.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using Piously.Game.Graphics.Containers;
using Piously.Game.Input.Bindings;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class TestScreen : Screen, IKeyBindingHandler<GlobalAction>
    {
        private bool loadComplete = false;

        private Hexagon hexagon;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Margin = new MarginPadding { Top = 20 },
                Children = new Drawable[]
                {
                    hexagon = new Hexagon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(500, 500),
                        Colour = Color4.Tomato
                    },
                    new TestClickableContainer
                    {
                        Child = new Trapezoid
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(500, 500),
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

        protected override void LoadComplete()
        {
            base.LoadComplete();
            loadComplete = true;
        }

        public bool OnPressed(GlobalAction action)
        {
            if (action == GlobalAction.TestAction1)
            {
                if (loadComplete) hexagon.Colour = Color4.LimeGreen;
                return true;
            }
            return false;
        }

        public void OnReleased(GlobalAction action)
        {
            if(action == GlobalAction.TestAction1)
                    if (loadComplete) hexagon.Colour = Color4.Tomato;
        }
        
        public void rotateHexagon()
        {
            if(loadComplete) hexagon.Rotation += (float)Time.Elapsed / 10;
        }
    }
}
