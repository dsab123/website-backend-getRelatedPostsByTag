using System;
using Newtonsoft.Json;

namespace GetRelatedPostsByTag.Models
{
  public class Tag
  {
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonConstructor]
    public Tag(int id, string name)
    {
      this.Id = id;
      this.Name = name;
    }

    [JsonConstructor]
    public Tag()
    {
      this.Id = 0;
      this.Name = "";
    }
  }
}
