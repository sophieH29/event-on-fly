//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System.IO;

//namespace EventOnFly.Data
//{
//    public class DesignTimeEofDbContextFactory : IDesignTimeDbContextFactory<EofDbContext>
//    {
//        public EofDbContext CreateDbContext(string[] args)
//        {
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();

//            var builder = new DbContextOptionsBuilder<EofDbContext>();

//            var connectionString = configuration.GetConnectionString("DefaultConnection");

//            builder.UseSqlServer(connectionString);

//            return new EofDbContext(builder.Options);
//        }
//    }
//}
