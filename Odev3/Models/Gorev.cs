namespace Odev3.Models
{
    public class Gorev
    {
        public string Id { get; set; } // Firebase Key'i için
        public string Baslik { get; set; }
        public string Detay { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan Saat { get; set; }
        public bool YapildiMi { get; set; }
    }
}