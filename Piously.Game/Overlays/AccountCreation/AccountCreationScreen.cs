using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Piously.Game.Overlays.AccountCreation
{
    public abstract class AccountCreationScreen : Screen
    {
        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            this.FadeOut().Delay(200).FadeIn(200);
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);
            this.FadeIn(200);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);
            this.FadeOut(200);
        }
    }
}
