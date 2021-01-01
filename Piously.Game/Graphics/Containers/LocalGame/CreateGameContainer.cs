using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Piously.Game.Graphics.Containers.LocalGame.CreateGame;
using osuTK;

namespace Piously.Game.Graphics.Containers.LocalGame
{
    public class CreateGameContainer : VisibilityContainer
    {
        public bool isVisible = false;

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            Anchor = Anchor.CentreRight;
            Origin = Anchor.CentreRight;
            Size = new Vector2(550, 575);
            Position = new Vector2(0, 0);
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = new PiouslyColour().Gray1,
                Radius = 10,
                Roundness = 0.6f,
            };
            Children = new Drawable[]
            {
                // Background
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Colour4(0.2f, 0.2f, 0.2f, 0.4f),
                },

                // GameRules
                new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0f, 0.025f),
                    Font = new FontUsage("Aller", 48, "Bold", false, false),
                    Text = "Game Rules",
                },

                // TimerContainer
                new TimerContainer(),

                // SaveNameContainer
                new SaveNameContainer(),

                // TwoPlayerContainer
                new TwoPlayerContainer(),

                // StartButtonContainer
                new StartButtonContainer(),
            };
        }

        protected override void PopIn()
        {
            this.MoveTo(new Vector2(0, 0), 200, Easing.OutQuad);
            isVisible = true;
        }
        
        protected override void PopOut()
        {
            this.MoveTo(new Vector2(1000, 0), 200, Easing.OutQuad);
            isVisible = false;
        }
    }
}
