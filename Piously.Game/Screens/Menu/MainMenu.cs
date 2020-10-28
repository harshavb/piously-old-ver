using System.Linq;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osu.Framework.Screens;
using Piously.Game.Configuration;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Containers;
using Piously.Game.IO;
using Piously.Game.Online.API;
using Piously.Game.Overlays;
using Piously.Game.Screens.Backgrounds;

namespace Piously.Game.Screens.Menu
{
    public class MainMenu : PiouslyScreen
    {
        public const float FADE_IN_DURATION = 300;

        public const float FADE_OUT_DURATION = 400;

        public override bool HideOverlaysOnEnter => buttons == null || buttons.State == ButtonSystemState.Initial;

        public override bool AllowBackButton => false;

        public override bool AllowExternalScreenChange => true;

        public override bool AllowRateAdjustments => false;

        private Screen songSelect;

        private ButtonSystem buttons;

        [Resolved]
        private GameHost host { get; set; }

        [Resolved(canBeNull: true)]
        private LoginOverlay login { get; set; }

        [Resolved]
        private IAPIProvider api { get; set; }

        [Resolved(canBeNull: true)]
        private DialogOverlay dialogOverlay { get; set; }

        private BackgroundScreenDefault background;

        protected override BackgroundScreen CreateBackground() => background;

        private Bindable<float> holdDelay;
        private Bindable<bool> loginDisplayed;

        private ExitConfirmOverlay exitConfirmOverlay;

        private ParallaxContainer buttonsContainer;

        //TO BE IMPLEMENTED
        [BackgroundDependencyLoader(true)]
        private void load(SettingsOverlay settings, PiouslyConfigManager config, SessionStatics statics) //RankingsOverlay rankings,
        {
            holdDelay = config.GetBindable<float>(PiouslySetting.UIHoldActivationDelay);
            loginDisplayed = statics.GetBindable<bool>(Static.LoginOverlayDisplayed);

            if (host.CanExit)
            {
                AddInternal(exitConfirmOverlay = new ExitConfirmOverlay
                {
                    Action = () =>
                    {
                        if (holdDelay.Value > 0)
                            confirmAndExit();
                        else
                            this.Exit();
                    }
                });
            }

            AddRangeInternal(new[]
            {
                buttonsContainer = new ParallaxContainer
                {
                    ParallaxAmount = 0.01f,
                    Children = new Drawable[]
                    {
                        buttons = new ButtonSystem
                        {
                            OnSolo = onSolo,
                            OnMulti = delegate { /*this.Push(new Multiplayer());*/ },
                            OnExit = confirmAndExit,
                        }
                    }
                },
                exitConfirmOverlay?.CreateProxy() ?? Drawable.Empty()
            });

            buttons.StateChanged += state =>
            {
                switch (state)
                {
                    case ButtonSystemState.Initial:
                    case ButtonSystemState.Exit:
                        Background.FadeColour(Color4.White, 500, Easing.OutSine);
                        break;

                    default:
                        Background.FadeColour(PiouslyColor.Gray(0.8f), 500, Easing.OutSine);
                        break;
                }
            };

            buttons.OnSettings = () => settings?.ToggleVisibility();
            //buttons.OnChart = () => rankings?.ShowSpotlights();

            LoadComponentAsync(background = new BackgroundScreenDefault());
        }

        [Resolved(canBeNull: true)]
        private PiouslyGame game { get; set; }

        private void confirmAndExit()
        {
            if (exitConfirmed) return;

            exitConfirmed = true;
            game?.PerformFromScreen(menu => menu.Exit());
        }

        public void LoadToSolo() => Schedule(onSolo);

        private void onSolo() => this.Push(consumeSongSelect());

        private Screen consumeSongSelect()
        {
            var s = songSelect;
            songSelect = null;
            return s;
        }

        [Resolved]
        private Storage storage { get; set; }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            buttons.FadeInFromZero(500);

            if (storage is PiouslyStorage osuStorage && osuStorage.Error != PiouslyStorageError.None)
                dialogOverlay?.Push(new StorageErrorDialog(osuStorage, osuStorage.Error));
        }

        private bool exitConfirmed;

        protected override void LogoArriving(PiouslyLogo logo, bool resuming)
        {
            base.LogoArriving(logo, resuming);

            buttons.SetPiouslyLogo(logo);

            logo.FadeColour(Color4.White, 100, Easing.OutQuint);
            logo.FadeIn(100, Easing.OutQuint);

            if (resuming)
            {
                buttons.State = ButtonSystemState.TopLevel;

                this.FadeIn(FADE_IN_DURATION, Easing.OutQuint);
                buttonsContainer.MoveTo(new Vector2(0, 0), FADE_IN_DURATION, Easing.OutQuint);
            }
            else if (!api.IsLoggedIn)
            {
                logo.Action += displayLogin;
            }

            bool displayLogin()
            {
                if (!loginDisplayed.Value)
                {
                    Scheduler.AddDelayed(() => login?.Show(), 500);
                    loginDisplayed.Value = true;
                }

                return true;
            }
        }

        protected override void LogoSuspending(PiouslyLogo logo)
        {
            var seq = logo.FadeOut(300, Easing.InSine)
                          .ScaleTo(0.2f, 300, Easing.InSine);

            seq.OnComplete(_ => buttons.SetPiouslyLogo(null));
            seq.OnAbort(_ => buttons.SetPiouslyLogo(null));
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);

            buttons.State = ButtonSystemState.EnteringMode;

            this.FadeOut(FADE_OUT_DURATION, Easing.InSine);
            buttonsContainer.MoveTo(new Vector2(-800, 0), FADE_OUT_DURATION, Easing.InSine);
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);

            (Background as BackgroundScreenDefault)?.Next();
        }

        public override bool OnExiting(IScreen next)
        {
            if (!exitConfirmed && dialogOverlay != null)
            {
                if (dialogOverlay.CurrentDialog is ConfirmExitDialog exitDialog)
                {
                    exitConfirmed = true;
                    exitDialog.Buttons.First().Click();
                }
                else
                {
                    dialogOverlay.Push(new ConfirmExitDialog(confirmAndExit, () => exitConfirmOverlay.Abort()));
                    return true;
                }
            }

            buttons.State = ButtonSystemState.Exit;
            OverlayActivationMode.Value = OverlayActivation.Disabled;

            this.FadeOut(3000);
            return base.OnExiting(next);
        }
    }
}
