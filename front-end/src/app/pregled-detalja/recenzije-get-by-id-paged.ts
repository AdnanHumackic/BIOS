export interface RecenzijePaged {
  recenzije: Recenzije
}

export interface Recenzije {
  dataItems: DataItemRecenzije[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItemRecenzije {
  id: number
  sadrzaj: string
  datumDodavanja: string
  evidentiraoKorisnikId: number
  artikalId: number
  korisnickoIme: string
}
