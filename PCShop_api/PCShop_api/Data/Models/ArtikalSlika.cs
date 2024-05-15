using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class ArtikalSlika
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Artikal))]
        public int ArtikalID { get; set; }
        public Artikal Artikal { get; set; }
        public string SlikaArtikla { get; set; }

    }
}
