using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Screens;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Sprites;
using Piously.Game.Online.API;
using Piously.Game.Overlays.Settings;
using Piously.Game.Screens.Menu;
using osuTK;
using osuTK.Graphics;

namespace Piously.Game.Overlays.AccountCreation
{
    public class ScreenWarning : AccountCreationScreen
    {
        private PiouslyTextFlowContainer multiAccountExplanationText;
        private LinkFlowContainer furtherAssistance;

        [Resolved(CanBeNull = true)]
        private IAPIProvider api { get; set; }

        private const string help_centre_url = "/help/wiki/Help_Centre#login";

        public override void OnEntering(IScreen last)
        {
            if (string.IsNullOrEmpty(api.ProvidedUsername))
            {
                this.FadeOut();
                this.Push(new ScreenEntry());
                return;
            }

            base.OnEntering(last);
        }

        [BackgroundDependencyLoader(true)]
        private void load(PiouslyColor colors, PiouslyGame game, TextureStore textures)
        {
            if (string.IsNullOrEmpty(api.ProvidedUsername))
                return;

            InternalChildren = new Drawable[]
            {
                new Sprite
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Texture = textures.Get(@"Menu/dev-build-footer"),
                },
                new Sprite
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Texture = textures.Get(@"Menu/dev-build-footer"),
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Padding = new MarginPadding(20),
                    Spacing = new Vector2(0, 5),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 150,
                            Child = new PiouslyLogo
                            {
                                Scale = new Vector2(0.1f),
                                Anchor = Anchor.Centre,
                                Hexagons = false,
                            },
                        },
                        new PiouslySpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Colour = Color4.Red,
                            Font = PiouslyFont.GetFont(size: 28, weight: FontWeight.Light),
                            Text = "Warning! 注意！",
                        },
                        multiAccountExplanationText = new PiouslyTextFlowContainer(cp => cp.Font = cp.Font.With(size: 12))
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y
                        },
                        new SettingsButton
                        {
                            Text = "Help, I can't access my account!",
                            Margin = new MarginPadding { Top = 50 },
                            Action = () => game?.OpenUrlExternally(help_centre_url)
                        },
                        new DangerousSettingsButton
                        {
                            Text = "I understand. This account isn't for me.",
                            Action = () => this.Push(new ScreenEntry())
                        },
                        furtherAssistance = new LinkFlowContainer(cp => cp.Font = cp.Font.With(size: 12))
                        {
                            Margin = new MarginPadding { Top = 20 },
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            AutoSizeAxes = Axes.Both
                        },
                    }
                }
            };

            multiAccountExplanationText.AddText("Are you ");
            multiAccountExplanationText.AddText(api.ProvidedUsername, cp => cp.Colour = colors.BlueLight);
            multiAccountExplanationText.AddText("? osu! has a policy of ");
            multiAccountExplanationText.AddText("one account per person!", cp => cp.Colour = colors.Yellow);
            multiAccountExplanationText.AddText(" Please be aware that creating more than one account per person may result in ");
            multiAccountExplanationText.AddText("permanent deactivation of accounts", cp => cp.Colour = colors.Yellow);
            multiAccountExplanationText.AddText(".");

            furtherAssistance.AddText("Need further assistance? Contact us via our ");
            furtherAssistance.AddLink("support system", help_centre_url);
            furtherAssistance.AddText(".");
        }
    }
}
