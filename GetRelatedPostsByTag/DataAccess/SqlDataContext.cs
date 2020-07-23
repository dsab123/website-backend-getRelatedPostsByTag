using Npgsql;
using System;
using System.Text;
using Amazon.Lambda.Core;
using System.Collections.Generic;

using GetRelatedPostsByTag.Models;
using GetRelatedPostsByTag.Utility;
using GetRelatedPostsByTag.Configuration;

namespace GetRelatedPostsByTag.DataAccess
{
  public class SqlDataContext : ISqlDataContext, IDisposable
  {
    private readonly ILambdaConfiguration _lambdaConfiguration;
    private readonly IExceptionLogFormatter _exceptionLogFormatter;
    private NpgsqlConnection Connection { get; }

    public SqlDataContext(ILambdaConfiguration lambdaConfiguration, IExceptionLogFormatter exceptionLogFormatter)
    {
      _lambdaConfiguration = lambdaConfiguration;
      _exceptionLogFormatter = exceptionLogFormatter;

      Connection = CreateConnection();
    }

    public List<BlogPostInfo> GetBlogPostsByTag(Tag tag)
    {
      var blogposts = new List<BlogPostInfo>();

      try
      {
        using (var command = new NpgsqlCommand(
   @$"select bi.blogpost_id, bi.slug, bi.title, bi.teaser
            from blogpostinfo bi
            join blogpostid_tag bt on bi.blogpost_id = bt.blogpost_id
            join tag t on bt.tag_id = t.tag_id
            where t.tag_name = lower('{tag.Name}')",
          Connection))
        {
          Connection.Open();
          var reader = command.ExecuteReader();

          while (reader.Read())
          {
          
            var id = int.Parse(reader["blogpost_id"].ToString());
            var slug = reader["slug"].ToString();
            var title = reader["title"].ToString();
            var teaser = reader["teaser"].ToString();
            blogposts.Add(new BlogPostInfo(id, slug, title, teaser));
          }
        }
      }
      catch (Exception ex)
      {
        LambdaLogger.Log(_exceptionLogFormatter.FormatExceptionLogMessage(ex));
      }

      Connection.Close();
      return blogposts;
    }

    private NpgsqlConnection CreateConnection()
    {
      if (Connection != null)
      {
        return Connection;
      }

      try
      {
        var section = _lambdaConfiguration.Configuration.GetSection("AppSettings");

        var server = section["Server"];
        var username = section["Username"];
        var database = section["Database"];
        var password = section["Password"];

        return new NpgsqlConnection(string.Format($"Database={database};Host={server};User ID={username};Password={password}"));
      }
      catch (Exception ex)
      {
        LambdaLogger.Log(_exceptionLogFormatter.FormatExceptionLogMessage(ex, new StringBuilder("ConnectionString was not retrieved from configuration, probably.")));
        throw;
      }
    }

    public void Dispose()
    {
      Connection.Dispose();
    }
  }
}
