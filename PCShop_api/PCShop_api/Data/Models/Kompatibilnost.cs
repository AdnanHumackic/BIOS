using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCShop_api.Data.Models
{
    public class Kompatibilnost
    {
        [Key]
        public int ArtikalKompatibilnostID { get; set; }

        [ForeignKey(nameof(Artikal))]
        public int Artikal1ID { get; set; }
        public Artikal Artikal1 { get; set; }


        [ForeignKey(nameof(Artikal))]
        public int Artikal2ID { get; set; }
        public Artikal Artikal2 { get; set; }
    }
}
