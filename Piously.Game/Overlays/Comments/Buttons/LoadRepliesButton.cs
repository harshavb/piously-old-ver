using osu.Framework.Graphics;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Comments.Buttons
{
    public class LoadRepliesButton : LoadingButton
    {
        private ButtonContent content;

        public LoadRepliesButton()
        {
            AutoSizeAxes = Axes.Both;
        }

        protected override Drawable CreateContent() => content = new ButtonContent();

        protected override void OnLoadStarted() => content.ToggleTextVisibility(false);

        protected override void OnLoadFinished() => content.ToggleTextVisibility(true);

        private class ButtonContent : CommentRepliesButton
        {
            public ButtonContent()
            {
                Text = "load replies";
            }
        }
    }
}
