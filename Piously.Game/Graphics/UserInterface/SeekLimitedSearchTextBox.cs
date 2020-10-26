namespace Piously.Game.Graphics.UserInterface
{
    /// <summary>
    /// A <see cref="SearchTextBox"/> which does not handle left/right arrow keys for seeking.
    /// </summary>
    public class SeekLimitedSearchTextBox : SearchTextBox
    {
        public override bool HandleLeftRightArrows => false;
    }
}
