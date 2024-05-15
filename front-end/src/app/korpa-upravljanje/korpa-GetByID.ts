export interface KorpaGetByIDResponse {
   korpa: KorpaGetByIDResponseKorpa[];
}

export interface KorpaGetByIDResponseKorpa {
   id: number;
   artikalID: number;
   imeArtikla: string;
   cijena: number;
   opis: string;
   datumDodavanja: Date;
}
