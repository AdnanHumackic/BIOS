export interface Narudzbe {
  narudzbe: NarudzbeResponse
}

export interface NarudzbeResponse {
  dataItems: DataItemNarudzbe[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItemNarudzbe {
  id: number
  ime: string
  prezime: string
  adresa: string
  brojTelefona: string
  dostavljac: string
  ukupnaCijena: number
}
