export interface ArtikliResponse {
  artikli: ArtikliResponseObrisani
}

export interface ArtikliResponseObrisani {
  dataItems: DataItemObrisani[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItemObrisani {
  id: number
  naziv: string
  cijena: number
  proizvodjac: string
  tip: string
  opis: string
  isObrisan: boolean
}
