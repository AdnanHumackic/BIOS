export interface ArtikliGetAllResponse {
  artikli: ArtikliGetAllResponseArtikli[]
}

export interface ArtikliGetAllResponseArtikli {
  id: number
  naziv: string
  cijena: number
  proizvodjac: string
  tip: string
  opis: string
  slika: string
}
