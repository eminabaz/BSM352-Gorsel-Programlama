using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Odev3.Models
{
    public class CurrencyItem
    {
        public CurrencyItem() { }
        public string Tur { get; set; }        // Ekranda görünen isim (Dolar, Euro vb.)
        public string Alis { get; set; }       // Alış Fiyatı
        public string Satis { get; set; }      // Satış Fiyatı
        public string Fark { get; set; }   // Değişim Oranı
        public string Yon { get; set; } // Ok İşareti (▲ veya ▼)
        public string Renk { get; set; }
    }
 } 

