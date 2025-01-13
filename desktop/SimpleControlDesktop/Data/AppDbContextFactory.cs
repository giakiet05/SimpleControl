using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Data
{
    public class AppDbContextFactory
    {
        private readonly string _dbName = "SimpleControl.db";
        private static readonly Lazy<AppDbContextFactory> _instance = new Lazy<AppDbContextFactory>(() => new AppDbContextFactory());
        public static AppDbContextFactory Instance => _instance.Value;
        public AppDbContext CreateDbContext()
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dbName);
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite($"Data Source={dbPath}").Options;
            return new AppDbContext(options);
        }
    }
}
