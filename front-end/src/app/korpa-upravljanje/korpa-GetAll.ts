export interface StavkeKorpaResponse {
  stavkeKorpa: StavkeKorpaResponseStavke[]
}

export interface StavkeKorpaResponseStavke {
  artikalID: number
  imeArtikla: string
  cijena: number
  opis: string
  datumDodavanja: string
}
