using System.Collections.Generic;

using GetRelatedPostsByTag.Models;

namespace GetRelatedPostsByTag.DataAccess
{
  public interface ISqlDataContext
  {
    List<BlogPostInfo> GetBlogPostsByTag(Tag tag);
  }
}