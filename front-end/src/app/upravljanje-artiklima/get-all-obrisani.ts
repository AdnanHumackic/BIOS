export interface ObrisaniArtGetAllResponse {
  obrisaniArt: ObrisaniArtGetAllResponseObrisani[]
}

export interface ObrisaniArtGetAllResponseObrisani {
  id: number
  naziv: string
  cijena: number
  proizvodjac: string
  tip: string
  opis: string
  isObrisan: boolean
}
