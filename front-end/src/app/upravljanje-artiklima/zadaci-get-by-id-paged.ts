export interface ZadaciPaged {
  zadaci: Zadaci
}

export interface Zadaci {
  dataItems: DataItemZadaci[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItemZadaci {
  id: number
  naziv: string
  opis: string
  datumDodavanja: string
  datumZavrsetka: string
}
