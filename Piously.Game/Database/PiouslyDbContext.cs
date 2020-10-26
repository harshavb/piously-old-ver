using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using osu.Framework.Logging;
using osu.Framework.Statistics;
using Piously.Game.Configuration;
using Piously.Game.IO;
using DatabasedKeyBinding = Piously.Game.Input.Bindings.DatabasedKeyBinding;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Piously.Game.Database
{
    public class PiouslyDbContext : DbContext
    {
        public DbSet<DatabasedKeyBinding> DatabasedKeyBinding { get; set; }

        public DbSet<DatabasedSetting> DatabasedSetting { get; set; }
        public DbSet<FileInfo> FileInfo { get; set; }

        private readonly string connectionString;

        private static readonly Lazy<PiouslyDbLoggerFactory> logger = new Lazy<PiouslyDbLoggerFactory>(() => new PiouslyDbLoggerFactory());

        private static readonly GlobalStatistic<int> contexts = GlobalStatistics.Get<int>("Database", "Contexts");

        static PiouslyDbContext()
        {
            // required to initialise native SQLite libraries on some platforms.
            SQLitePCL.Batteries_V2.Init();

            // https://github.com/aspnet/EntityFrameworkCore/issues/9994#issuecomment-508588678
            SQLitePCL.raw.sqlite3_config(2 /*SQLITE_CONFIG_MULTITHREAD*/);
        }

        /// <summary>
        /// Create a new in-memory OsuDbContext instance.
        /// </summary>
        public PiouslyDbContext()
            : this("DataSource=:memory:")
        {
            // required for tooling (see https://wildermuth.com/2017/07/06/Program-cs-in-ASP-NET-Core-2-0).

            Migrate();
        }

        /// <summary>
        /// Create a new OsuDbContext instance.
        /// </summary>
        /// <param name="connectionString">A valid SQLite connection string.</param>
        public PiouslyDbContext(string connectionString)
        {
            this.connectionString = connectionString;

            var connection = Database.GetDbConnection();

            try
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "PRAGMA journal_mode=WAL;";
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                connection.Close();
                throw;
            }

            contexts.Value++;
        }

        ~PiouslyDbContext()
        {
            // DbContext does not contain a finalizer (https://github.com/aspnet/EntityFrameworkCore/issues/8872)
            // This is used to clean up previous contexts when fresh contexts are exposed via DatabaseContextFactory
            Dispose();
        }

        private bool isDisposed;

        public override void Dispose()
        {
            if (isDisposed) return;

            isDisposed = true;

            base.Dispose();

            contexts.Value--;
            GC.SuppressFinalize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                // this is required for the time being due to the way we are querying in places like BeatmapStore.
                // if we ever move to having consumers file their own .Includes, or get eager loading support, this could be re-enabled.
                .UseSqlite(connectionString, sqliteOptions => sqliteOptions.CommandTimeout(10))
                .UseLoggerFactory(logger.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabasedKeyBinding>().HasIndex(b => new DatabasedKeyBinding());
            modelBuilder.Entity<DatabasedKeyBinding>().HasIndex(b => b.IntAction);

            modelBuilder.Entity<DatabasedSetting>().HasIndex(b => new DatabasedSetting());

            modelBuilder.Entity<FileInfo>().HasIndex(b => b.Hash).IsUnique();
            modelBuilder.Entity<FileInfo>().HasIndex(b => b.ReferenceCount);
        }

        private class PiouslyDbLoggerFactory : ILoggerFactory
        {
            #region Disposal

            public void Dispose()
            {
            }

            #endregion

            public ILogger CreateLogger(string categoryName) => new PiouslyDbLogger();

            public void AddProvider(ILoggerProvider provider)
            {
                // no-op. called by tooling.
            }

            private class PiouslyDbLogger : ILogger
            {
                public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
                {
                    if (logLevel < LogLevel.Information)
                        return;

                    osu.Framework.Logging.LogLevel frameworkLogLevel;

                    switch (logLevel)
                    {
                        default:
                            frameworkLogLevel = osu.Framework.Logging.LogLevel.Debug;
                            break;

                        case LogLevel.Warning:
                            frameworkLogLevel = osu.Framework.Logging.LogLevel.Important;
                            break;

                        case LogLevel.Error:
                        case LogLevel.Critical:
                            frameworkLogLevel = osu.Framework.Logging.LogLevel.Error;
                            break;
                    }

                    Logger.Log(formatter(state, exception), LoggingTarget.Database, frameworkLogLevel);
                }

                public bool IsEnabled(LogLevel logLevel)
                {
#if DEBUG_DATABASE
                    return logLevel > LogLevel.Debug;
#else
                    return logLevel > LogLevel.Information;
#endif
                }

                public IDisposable BeginScope<TState>(TState state) => null;
            }
        }

        public void Migrate() => Database.Migrate();
    }
}