namespace Piously.Game.Configuration
{
    public class SessionStatics : InMemoryConfigManager<Static>
    {
        protected override void InitialiseDefaults()
        {
            Set(Static.LoginOverlayDisplayed, false);
        }
    }

    public enum Static
    {
        LoginOverlayDisplayed,
    }
}
