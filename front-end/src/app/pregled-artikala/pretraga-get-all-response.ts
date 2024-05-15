export interface ArtikliGetAllPretragaResponse {
  artikli: ArtikliGetAllPretragaResponseArtikli[]
}

export interface ArtikliGetAllPretragaResponseArtikli {
  id: number
  naziv: string
  cijena: number
  proizvodjac: string
  tip: string
  opis: string
}
