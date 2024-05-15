namespace PCShop_api.Endpoint.Zadaci.GetByID
{
    public class ZadaciGetByIDResponse
    {
        public List<ZadaciGetByIDResponseZadaci> Zadaci { get; set; }
    }
    public class ZadaciGetByIDResponseZadaci
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        public DateTime DatumZavrsetka { get; set; }
    }
}
