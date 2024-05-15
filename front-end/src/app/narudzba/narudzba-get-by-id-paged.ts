export interface NarudzbaResponse {
  narudzba: NarudzbaResponsePaged
}

export interface NarudzbaResponsePaged {
  dataItems: DataItemNarudzba[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItemNarudzba {
  id: number
  ime: string
  prezime: string
  adresa: string
  brojTelefona: string
  dostavljac: string
  ukupnaCijena: number
}
