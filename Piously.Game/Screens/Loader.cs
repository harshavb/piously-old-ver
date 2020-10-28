using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Utils;
using Piously.Game.Screens.Menu;
using osu.Framework.Screens;
using osu.Framework.Threading;
using Piously.Game.Configuration;
using Piously.Game.Graphics.UserInterface;
using IntroSequence = Piously.Game.Configuration.IntroSequence;

namespace Piously.Game.Screens
{
    public class Loader : StartupScreen
    {
        private bool showDisclaimer;

        public Loader()
        {
            ValidForResume = false;
        }

        private PiouslyScreen loadableScreen;
        private ShaderPrecompiler precompiler;

        private IntroSequence introSequence;
        private LoadingSpinner spinner;
        private ScheduledDelegate spinnerShow;

        protected virtual PiouslyScreen CreateLoadableScreen()
        {
            return getIntroSequence();
        }

        private IntroScreen getIntroSequence()
        {
            if (introSequence == IntroSequence.Random)
                introSequence = (IntroSequence)RNG.Next(0, (int)IntroSequence.Random);

            switch (introSequence)
            {
                case IntroSequence.Circles:
                    return new IntroCircles();

                case IntroSequence.Welcome:
                    return new IntroWelcome();

                default:
                    return new IntroTriangles();
            }
        }

        protected virtual ShaderPrecompiler CreateShaderPrecompiler() => new ShaderPrecompiler();

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            LoadComponentAsync(precompiler = CreateShaderPrecompiler(), AddInternal);
            LoadComponentAsync(loadableScreen = CreateLoadableScreen());

            LoadComponentAsync(spinner = new LoadingSpinner(true, true)
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                Margin = new MarginPadding(40),
            }, _ =>
            {
                AddInternal(spinner);
                spinnerShow = Scheduler.AddDelayed(spinner.Show, 200);
            });

            checkIfLoaded();
        }

        private void checkIfLoaded()
        {
            if (loadableScreen.LoadState != LoadState.Ready || !precompiler.FinishedCompiling)
            {
                Schedule(checkIfLoaded);
                return;
            }

            spinnerShow?.Cancel();

            if (spinner.State.Value == Visibility.Visible)
            {
                spinner.Hide();
                Scheduler.AddDelayed(() => this.Push(loadableScreen), LoadingSpinner.TRANSITION_DURATION);
            }
            else
                this.Push(loadableScreen);
        }

        [BackgroundDependencyLoader]
        private void load(PiouslyGameBase game, PiouslyConfigManager config)
        {
            showDisclaimer = game.IsDeployedBuild;
            introSequence = config.Get<IntroSequence>(PiouslySetting.IntroSequence);
        }

        /// <summary>
        /// Compiles a set of shaders before continuing. Attempts to draw some frames between compilation by limiting to one compile per draw frame.
        /// </summary>
        public class ShaderPrecompiler : Drawable
        {
            private readonly List<IShader> loadTargets = new List<IShader>();

            public bool FinishedCompiling { get; private set; }

            [BackgroundDependencyLoader]
            private void load(ShaderManager manager)
            {
                loadTargets.Add(manager.Load(VertexShaderDescriptor.TEXTURE_2, FragmentShaderDescriptor.TEXTURE_ROUNDED));
                loadTargets.Add(manager.Load(VertexShaderDescriptor.TEXTURE_2, FragmentShaderDescriptor.BLUR));
                loadTargets.Add(manager.Load(VertexShaderDescriptor.TEXTURE_2, FragmentShaderDescriptor.TEXTURE));

                loadTargets.Add(manager.Load(@"CursorTrail", FragmentShaderDescriptor.TEXTURE));

                loadTargets.Add(manager.Load(VertexShaderDescriptor.TEXTURE_3, FragmentShaderDescriptor.TEXTURE_ROUNDED));
                loadTargets.Add(manager.Load(VertexShaderDescriptor.TEXTURE_3, FragmentShaderDescriptor.TEXTURE));
            }

            protected virtual bool AllLoaded => loadTargets.All(s => s.IsLoaded);

            protected override void Update()
            {
                base.Update();

                // if our target is null we are done.
                if (AllLoaded)
                {
                    FinishedCompiling = true;
                    Expire();
                }
            }
        }
    }
}
