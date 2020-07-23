﻿using Newtonsoft.Json;

namespace GetRelatedPostsByTag.Models
{
  // TODO: figure out how to use Lambda Layers, because this is the same as the BlogPostInfo model in the getBlogPostLookup lambda
  public class BlogPostInfo
  {
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }
    [JsonProperty(PropertyName = "slug")]
    public string Slug { get; set; }
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }
    [JsonProperty(PropertyName = "teaser")]
    public string Teaser { get; set; }

    [JsonConstructor]
    public BlogPostInfo(int id, string slug, string title, string teaser)
    {
      Id = id;
      Slug = slug;
      Title = title;
      Teaser = teaser;
    }
  }
}
