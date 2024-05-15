export interface WishlistGetByIdResponse {
  wishlist: WishlistGetByIdResponseWishlist[];
}

export interface WishlistGetByIdResponseWishlist {
  artikalId: number;
  imeArtikla: string;
  proizvodjac: string;
  cijena: number;
  opis: string;
  datumDodavanja: string;
}
