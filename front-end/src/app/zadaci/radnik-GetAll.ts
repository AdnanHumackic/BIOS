export interface StavkeRadnikResponse {
  radnik: StavkeRadnikResponseStavke[]
}

export interface StavkeRadnikResponseStavke {
  id: number
  korisnickoIme: string
  ime: string
  prezime: string
  datumRodjenja: string
  datumZaposlenja: string
}
