export interface DrzavaGetAllResponse {
  drzava: DrzavaGetAllResponseDrzava[]
}

export interface DrzavaGetAllResponseDrzava {
  id: number
  naziv: string
  skracenica: any
}
