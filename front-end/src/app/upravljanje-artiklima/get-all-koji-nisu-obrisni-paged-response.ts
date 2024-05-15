export interface Artikli {
  artikli: ArtikliPaged
}

export interface ArtikliPaged  {
  dataItems: DataItem[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItem {
  id: number
  naziv: string
  cijena: number
  proizvodjac: string
  tip: string
  opis: string
  isObrisan: boolean
}
