namespace Piously.Game.Database
{
    public interface IDatabaseContextFactory
    {
        /// <summary>
        /// Get a context for read-only usage.
        /// </summary>
        PiouslyDbContext Get();

        /// <summary>
        /// Request a context for write usage. Can be consumed in a nested fashion (and will return the same underlying context).
        /// This method may block if a write is already active on a different thread.
        /// </summary>
        /// <param name="withTransaction">Whether to start a transaction for this write.</param>
        /// <returns>A usage containing a usable context.</returns>
        DatabaseWriteUsage GetForWrite(bool withTransaction = true);
    }
}
