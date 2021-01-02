using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using Piously.Game.Graphics.Backgrounds;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Screens;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Graphics.Containers.LocalGame.CreateGame
{
    public class StartButtonContainer : CircularContainer
    {
        protected Box Hover;

        protected PiouslyGame Game;

        [BackgroundDependencyLoader]
        private void load(PiouslyColour colour, PiouslyGame game)
        {
            Anchor = Anchor.BottomCentre;
            Origin = Anchor.BottomCentre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(0.6f, 0.1f);
            Position = new Vector2(0f, -0.025f);
            Masking = true;

            Game = game;

            Children = new Drawable[]
            {
                Hover = new Box
                {
                        Alpha = 0,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.White.Opacity(.1f),
                        Blending = BlendingParameters.Additive,
                        Depth = float.MinValue
                },
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = colour.Yellow,
                },
                new Hexagons
                {
                    HexagonScale = 2,
                    ColourLight = colour.YellowDark,
                    ColourDark = colour.YellowLight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f, 3f),
                },
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = new FontUsage("Aller", 40, "Bold", false, false),
                    Text = "START",
                },
                new PiouslyButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Colour = new Colour4(0, 0, 0, 0),
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            Hover.FadeIn(200, Easing.OutQuint);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            Hover.FadeOut(300);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            Content.ScaleTo(0.9f, 4000, Easing.OutQuint);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            Content.ScaleTo(1, 1000, Easing.OutElastic);

            if (IsHovered) Game.TransitionScreen(new LoadingScreen());

            base.OnMouseUp(e);
        }
    }
}
