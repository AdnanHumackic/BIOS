namespace PCShop_api.Endpoint.Artikal.GetByID
{
    public class ArtikalGetByIDResponse
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public string Proizvodjac { get; set; }
        public int TipID { get; set; } 
        //public string Slika { get; set; }
        public string Opis { get; set; }

    }
}
