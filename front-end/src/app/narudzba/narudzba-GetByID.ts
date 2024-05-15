export interface NarudzbaGetByIDResponse {
   narudzba: NarudzbaGetByIdResponseNarudzba[];
}

export interface NarudzbaGetByIdResponseNarudzba {
   id: number;
   ime: string;
   prezime: string;
   adresa: string;
   brojTelefona: string;
   dostavljac: string;
   ukupnaCijena: number;
}
