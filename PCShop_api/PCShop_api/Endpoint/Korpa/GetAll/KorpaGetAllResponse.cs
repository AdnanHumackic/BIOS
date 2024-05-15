using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Korpa.GetAll
{
    public class KorpaGetAllResponse
    {
        public List<KorpaGetAllResponseKorpa> StavkeKorpa { get; set; }

        public class KorpaGetAllResponseKorpa
        {
            public int ID { get; set; }
            public int ArtikalID { get; set; }
            public string ImeArtikla { get; set; }
            public int Cijena { get; set; }
            public string Opis { get; set; }
            //public string Slika { get; set; }
            public DateTime DatumDodavanja { get; set; }
        }
    }
}
