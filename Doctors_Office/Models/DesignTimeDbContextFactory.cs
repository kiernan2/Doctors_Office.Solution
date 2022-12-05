using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Doctors_Office.Models
{
  public class Doctors_OfficeContextFactory : IDesignTimeDbContextFactory<Doctors_OfficeContext>
  {
    Doctors_OfficeContext IDesignTimeDbContextFactory<Doctors_OfficeContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      DbContextOptionsBuilder<Doctors_OfficeContext> builder = new DbContextOptionsBuilder<Doctors_OfficeContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new Doctors_OfficeContext(builder.Options);
    }
  }
}