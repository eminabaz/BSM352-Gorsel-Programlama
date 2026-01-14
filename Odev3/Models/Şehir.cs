using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev3.Models
{
    public class Sehir
    {
        public string Name { get; set; } // Ekranda görünen isim (Örn: İSTANBUL)

        // API'ye gönderilecek URL (Otomatik hesaplanır)
        public string SourceUrl
        {
            get
            {
                // Türkçe karakterleri düzelt (Ödev Kuralı: Dikkat-1)
                string duzgunIsim = ConvertToEnglishCharacters(Name.ToUpper());

                // MGM API Linki
                return $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={duzgunIsim}&basla=1&bitir=5&rC=111&rZ=fff";
            }
        }

        // Türkçe karakterleri İngilizce karşılıklarına çeviren yardımcı metot
        private string ConvertToEnglishCharacters(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

            return text
                .Replace('Ç', 'C').Replace('ç', 'c')
                .Replace('Ğ', 'G').Replace('ğ', 'g')
                .Replace('I', 'I').Replace('ı', 'i') // MGM 'I' harfini sever
                .Replace('İ', 'I').Replace('i', 'i')
                .Replace('Ö', 'O').Replace('ö', 'o')
                .Replace('Ş', 'S').Replace('ş', 's')
                .Replace('Ü', 'U').Replace('ü', 'u');
        }
    }
}
