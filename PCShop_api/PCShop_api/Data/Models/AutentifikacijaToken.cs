using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PCShop_api.Data.Models
{
    public class AutentifikacijaToken
    {
        [Key]
        public int ID { get; set; }
        public string Vrijednost { get; set; }
        [ForeignKey(nameof(korisnickiNalog))]
        public int KorisnickiNalogID { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }
        public DateTime vrijemeEvidentiranja { get; set; }
        public string? ipAdresa { get; set; }
        [JsonIgnore]
        public string? TwoFKey { get; set; }
        public bool Is2FOtkljucano { get; set; }
    }
}
