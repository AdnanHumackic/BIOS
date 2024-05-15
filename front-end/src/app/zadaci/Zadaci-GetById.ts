export interface ZadaciGetByIdResponse {
  zadaci: ZadaciGetByIdResponseZadaci[]
}

export interface ZadaciGetByIdResponseZadaci {
  id: number
  naziv: string
  opis: string
  datumDodavanja: string
  datumZavrsetka: string
}
