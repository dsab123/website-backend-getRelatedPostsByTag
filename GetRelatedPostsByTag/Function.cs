using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using Amazon.Lambda.Core;
using GetRelatedPostsByTag.Models;
using GetRelatedPostsByTag.Utility;
using GetRelatedPostsByTag.DataAccess;
using GetRelatedPostsByTag.Configuration;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.CamelCaseLambdaJsonSerializer))]

namespace GetRelatedPostsByTag
{
  public class Function
  {
    public ISqlDataContext DataContext;
    private readonly IExceptionLogFormatter _exceptionLogFormatter;

    public Function()
    {
      var serviceCollection = new ServiceCollection();
      ConfigureServices(serviceCollection);
      var serviceProvider = serviceCollection.BuildServiceProvider();

      DataContext = serviceProvider.GetService<ISqlDataContext>();
      _exceptionLogFormatter = serviceProvider.GetService<IExceptionLogFormatter>();
    }

    /// <summary>
    /// Entry point to retrieve BlogPost information from database
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public List<BlogPostInfo> FunctionHandler(Tag tag)
    {
      LambdaLogger.Log("GetRelatedPostsByTag Lambda Started; tag is: " + tag.Id + "; " + tag.Name + "\n");

      try
      {
        LambdaLogger.Log("GetRelatedPostsByTag Lambda finishing \n");
        return DataContext.GetBlogPostsByTag(tag);
      }
      catch (Exception ex)
      {
        LambdaLogger.Log(_exceptionLogFormatter.FormatExceptionLogMessage(ex));
        throw;
      }
    }

    private void ConfigureServices(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient<ILambdaConfiguration, LambdaConfiguration>();
      serviceCollection.AddTransient<ISqlDataContext, SqlDataContext>();
      serviceCollection.AddTransient<IExceptionLogFormatter, ExceptionLogFormatter>();
    }

    // used in local testing
    public static void Main()
    {
      var ret = new Function();
      ret.FunctionHandler(null);
    }
  }
}
