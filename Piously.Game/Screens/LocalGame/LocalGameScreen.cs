using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using Piously.Game.Screens.Backgrounds;
using Piously.Game.Graphics.Containers.LocalGame;

namespace Piously.Game.Screens.Local
{
    public class LocalGameScreen : BackgroundScreen
    {

        public LocalGameScreen(bool animateOnEnter = true) : base(animateOnEnter, "Menu/load-game-background")
        {
            Alpha = 0;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;

            // ContentContainer
            AddInternal(new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.8f),

                Children = new Drawable[]
                {
                    // BackgroundDrawing
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        RelativePositionAxes = Axes.Both,
                        Size = new Vector2(1f),
                        Colour = new Colour4(0.25f, 0.25f, 0.25f, 0.4f),
                    },

                    // TitleContainer
                    new TitleContainer(),

                    // LeftPanelContainer
                    new LeftPanelContainer(),

                    // MainContentContainer
                    new GameRulesContainer(),
                }
            });
        }

        public override void OnEntering(IScreen last)
        {
            this.ScaleTo(1f, 500, Easing.None);
            this.FadeTo(1, 300, Easing.None);
        }

        public override bool OnExiting(IScreen last)
        {
            this.ScaleTo(0.5f, 500, Easing.None);
            this.FadeTo(0, 300, Easing.None);
            return false;
        }

        public override void OnResuming(IScreen last)
        {
            this.ScaleTo(1f, 500, Easing.None);
            this.FadeTo(1, 300, Easing.None);
        }

        public override void OnSuspending(IScreen last)
        {
            this.ScaleTo(0.5f, 500, Easing.None);
            this.FadeTo(0, 300, Easing.None);
        }
    }
}
