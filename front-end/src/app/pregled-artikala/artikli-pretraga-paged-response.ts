export interface ArtikalPretraga {
  artikal: ArtikalPretragaResponse
}

export interface ArtikalPretragaResponse {
  dataItems: DataItemPretraga[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItemPretraga {
  id: number
  naziv: string
  cijena: number
  proizvodjac: string
  tip: string
  opis: string
}
