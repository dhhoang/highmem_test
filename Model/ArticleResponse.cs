// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Doc
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("journal")]
    public string Journal { get; set; }

    [JsonPropertyName("eissn")]
    public string Eissn { get; set; }

    [JsonPropertyName("publication_date")]
    public DateTime PublicationDate { get; set; }

    [JsonPropertyName("article_type")]
    public string ArticleType { get; set; }

    [JsonPropertyName("author_display")]
    public List<string> AuthorDisplay { get; set; }

    [JsonPropertyName("abstract")]
    public List<string> Abstract { get; set; }

    [JsonPropertyName("title_display")]
    public string TitleDisplay { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }
}

public class Response
{
    [JsonPropertyName("numFound")]
    public int NumFound { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("maxScore")]
    public double MaxScore { get; set; }

    [JsonPropertyName("docs")]
    public List<Doc> Docs { get; set; }
}

public class ArticleResponse
{
    [JsonPropertyName("response")]
    public Response Response { get; set; }
}
