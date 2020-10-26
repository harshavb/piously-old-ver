namespace Piously.Game.Online.Chat
{
    /// <summary>
    /// A message which is generated and displayed locally.
    /// </summary>
    public class LocalMessage : Message
    {
        protected LocalMessage(long? id)
            : base(id)
        {
        }
    }
}
