export interface RecenzijaGetbyIdResponse {
  recenzija: Recenzija[]
}

export interface Recenzija {
  id: number
  sadrzaj: string
  datumDodavanja: string
  evidentiraoKorisnikId: number
  artikalId: number
  korisnickoIme: string
}
