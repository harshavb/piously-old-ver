using System;
using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK;

namespace Piously.Game.Screens.Backgrounds
{
    public class BackgroundScreen : Screen, IEquatable<BackgroundScreen>
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Random randomGenerator = new Random();
            int choice = randomGenerator.Next(1, 18);
            AddInternal(new Background("Menu/menu-background-" + choice));
        }

        private readonly bool animateOnEnter;

        public BackgroundScreen(bool animateOnEnter = true)
        {
            this.animateOnEnter = animateOnEnter;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        public virtual bool Equals(BackgroundScreen other)
        {
            return other?.GetType() == GetType();
        }

        private const float transition_length = 500;
        private const float x_movement_amount = 50;

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            // we don't want to handle escape key.
            return false;
        }

        protected override void Update()
        {
            base.Update();
            Scale = new Vector2(1 + x_movement_amount / DrawSize.X * 2);
        }

        public override void OnEntering(IScreen last)
        {
            if (animateOnEnter)
            {
                this.FadeOut();
                this.MoveToX(x_movement_amount);

                this.FadeIn(transition_length, Easing.InOutQuart);
                this.MoveToX(0, transition_length, Easing.InOutQuart);
            }

            base.OnEntering(last);
        }

        public override void OnSuspending(IScreen next)
        {
            this.MoveToX(-x_movement_amount, transition_length, Easing.InOutQuart);
            base.OnSuspending(next);
        }

        public override bool OnExiting(IScreen next)
        {
            this.FadeOut(transition_length, Easing.OutExpo);
            this.MoveToX(x_movement_amount, transition_length, Easing.OutExpo);

            return base.OnExiting(next);
        }

        public override void OnResuming(IScreen last)
        {
            this.MoveToX(0, transition_length, Easing.OutExpo);
            base.OnResuming(last);
        }
    }
}
