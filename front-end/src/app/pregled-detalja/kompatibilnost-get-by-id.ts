export interface KompGetByIDResponse {
  komp: KompatibilnostGetByIdResponseKompatibilnost[]
}

export interface KompatibilnostGetByIdResponseKompatibilnost {
  id: number
  kompatibilnostID: number
  artikal2Ime: string
}
