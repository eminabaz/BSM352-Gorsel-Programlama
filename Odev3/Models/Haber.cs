using Newtonsoft.Json;

namespace Odev3.Models
{
    public class RssRoot
    {
        public string Status { get; set; }
        public Feed Feed { get; set; }

        // JSON'da "items" küçük harfle gelir
        [JsonProperty("items")]
        public List<HaberItem> Items { get; set; }
    }

    public class Feed
    {
        public string Title { get; set; }
        public string Image { get; set; }
    }

    public class HaberItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("pubDate")]
        public string PubDate { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        // En önemli kısım burası. JSON'dan "enclosure" olarak gelir.
        [JsonProperty("enclosure")]
        public Enclosure Enclosure { get; set; }
    }

    public class Enclosure
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    // Kategori sınıfı aynı kalabilir
    public class HaberKategori
    {
        public string Baslik { get; set; }
        public string RssUrl { get; set; }
        public bool IsSelected { get; set; }
        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }
    }
}