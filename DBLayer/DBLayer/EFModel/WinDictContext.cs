using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("DBLayer.Tests")]

namespace Stachowski.WinDict.DBLayer.EFModel
{
    internal class WinDictContext : DbContext
    {
        public WinDictContext() : base(new LocalDbConnectionFactory(ConfigurationManager.AppSettings["localDbVersion"]).CreateConnection(ConfigurationManager.AppSettings["nameOrConnectionString"]), true)
        {
            
        }

        public DbSet<WordString> WordStrings { get; set; }
        public DbSet<WordInDictionary> Words { get; set; }
        public DbSet<WordStatisticsPerUser> Statistics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}
