using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Doctors_Office
{
  public class Program
  {
    public static void Main(string[] args)
    {
      IWebHost host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}