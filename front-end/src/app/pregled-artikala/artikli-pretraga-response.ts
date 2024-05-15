export interface ArtikalPretragaResponse {
  artikli: ArtikalPretragaResponseArtikal[]
}

export interface ArtikalPretragaResponseArtikal {
  id: number
  naziv: string
  cijena: number
  proizvodjac: any
  tip: string
  opis:string
}
