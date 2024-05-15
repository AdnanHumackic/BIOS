import {KorisnickiNalog} from "./korisnickiNalog";

export interface AutentifikacijaToken {
  id: number
  vrijednost: string
  korisnickiNalogID: number
  korisnickiNalog: KorisnickiNalog
  vrijemeEvidentiranja: string
  ipAdresa: string
  is2FOtkljucano: boolean
}
