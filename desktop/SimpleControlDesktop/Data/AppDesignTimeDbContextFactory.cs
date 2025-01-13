using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Data
{
    public class AppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly string _dbName = "SimpleControl.db";
        public AppDbContext CreateDbContext(string[] args)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dbName);
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite($"Data Source={dbPath}").Options;
            return new AppDbContext(options);
        }
    }
}
