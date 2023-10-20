using Newtonsoft.Json;

public class Words
{
    [JsonProperty("word")]
    public string Word { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
}