export interface StavkeZadatakResponse {
  stavkeZadatak: StavkeZadatakResponseStavke[]
}

export interface StavkeZadatakResponseStavke {
  id: number
  naziv: string
  opis: string
  datumDodavanja: string
  datumZavrsetka: string
}
