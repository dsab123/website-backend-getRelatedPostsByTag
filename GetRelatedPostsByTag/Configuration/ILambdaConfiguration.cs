using Microsoft.Extensions.Configuration;

namespace GetRelatedPostsByTag.Configuration
{
  public interface ILambdaConfiguration
  {
    IConfiguration Configuration { get; }
  }
}