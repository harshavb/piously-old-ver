using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using Piously.Game.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osu.Framework.Bindables;
using Humanizer;
using Piously.Game.Graphics.Sprites;

namespace Piously.Game.Overlays.Comments
{
    public class DeletedCommentsCounter : CompositeDrawable
    {
        public readonly BindableBool ShowDeleted = new BindableBool();

        public readonly BindableInt Count = new BindableInt();

        private readonly SpriteText countText;

        public DeletedCommentsCounter()
        {
            AutoSizeAxes = Axes.Both;
            InternalChild = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(3, 0),
                Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Icon = FontAwesome.Solid.Trash,
                        Size = new Vector2(14),
                    },
                    countText = new PiouslySpriteText
                    {
                        Font = PiouslyFont.GetFont(size: 14, weight: FontWeight.Bold, italics: true),
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Count.BindValueChanged(_ => updateDisplay(), true);
            ShowDeleted.BindValueChanged(_ => updateDisplay(), true);
        }

        private void updateDisplay()
        {
            if (!ShowDeleted.Value && Count.Value != 0)
            {
                countText.Text = @"deleted comment".ToQuantity(Count.Value);
                Show();
            }
            else
                Hide();
        }
    }
}
