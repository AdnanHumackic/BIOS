export interface Profili {
  korisnici: Korisnici[]
}

export interface Korisnici {
  id: number
  korisnickoIme: string
  slikaKorisnika: any
  isKupac: boolean
  isRadnik: boolean
  isAdmin: boolean
}
