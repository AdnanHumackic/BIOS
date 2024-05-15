export interface StavkeWishlistResponse {
  stavkeWishlist: StavkeWishlistResponseStavke[]
}

export interface StavkeWishlistResponseStavke {
  artikalId: number
  imeArtikla: string
  proizvodjac: string
  cijena: number
  opis: string
  datumDodavanja: string
}
