using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikullServices.Models
{
    public class Produkt
    {
        public int id { get; set; }
        public string Emertimi{ get; set; }
        public string Njesia { get; set; }
        public DateTime DataSkadences { get; set; }
        public double Cmimi { get; set; }
        public Lloj Lloj { get; set; }
        public bool KaTvsh { get; set; }
        public ETipiProdukt Tipi { get; set; }
        public string BarkodKod { get; set; }


    }
}
