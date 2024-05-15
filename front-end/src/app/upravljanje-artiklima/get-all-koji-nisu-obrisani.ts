export interface ArtikliGetAllKojiNisuObrisaniResponse {
  artikli: ArtikliGetAllKojiNisuObrisaniResponseArtikli[]
}

export interface ArtikliGetAllKojiNisuObrisaniResponseArtikli {
  id: number
  naziv: string
  cijena: number
  proizvodjac: string
  tip: string
  opis: string
  isObrisan: boolean
}
