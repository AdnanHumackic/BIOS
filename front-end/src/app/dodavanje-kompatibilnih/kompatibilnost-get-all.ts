export interface KompatibilnostGetAllResponse {
  kompatibilnost: KompatibilnostGetAllResponseKompatibilnost[]
}

export interface KompatibilnostGetAllResponseKompatibilnost {
  id: number
  artikal1: string
  artikal2: string
  art1ID: number
  art2ID: number
}
