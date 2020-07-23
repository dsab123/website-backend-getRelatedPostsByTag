using System.IO;
using Microsoft.Extensions.Configuration;

namespace GetRelatedPostsByTag.Configuration
{
  public class LambdaConfiguration : ILambdaConfiguration
  {
    public IConfiguration Configuration => new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();
  }
}