namespace PCShop_api.Endpoint.Wishlist.GetAll
{
    public class WishlistGetAllResponse
    {
        public List<WishlistGetAllResponseWishlist> StavkeWishlist { get; set; }

        public class WishlistGetAllResponseWishlist
        {
            public int ArtikalId { get; set; }
            public string ImeArtikla { get; set; }
            public string Proizvodjac { get; set; }
            public int Cijena { get; set; }
            public string Opis { get; set; }
            //public string Slika { get; set; }
            public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        }
    }
}
