import {Component, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from "@angular/common/http";
import { MojConfig } from "../moj-config";
//@ts-ignore
import { StavkeKorpaResponse, StavkeKorpaResponseStavke } from "./korpa-GetAll";
import { FormsModule } from "@angular/forms";
import { KorpaGetByIDResponse, KorpaGetByIDResponseKorpa } from "./korpa-GetByID";
import { MyAuthService } from "../shared-services/MyAuthService";
import { DialogServiceService } from "../shared-services/dialog-service.service";
import { MatOkDialogComponent } from "../mat-ok-dialog/mat-ok-dialog.component";
import { MatConfirmDialogComponent } from "../mat-confirm-dialog/mat-confirm-dialog.component";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { Router } from "@angular/router";
import {MAT_DIALOG_DATA, MatDialog, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {Dostavljac, DostavljacGetAllResponse} from "./dostavljaci-GetAll";
interface StavkeKorpaResponse {
  stavkeKorpa: StavkeKorpaResponseStavke[];
}
export interface KorpaItem {
  ImeArtikla: string;
  Proizvodjac: string;
  Cijena: number;
  Opis: string;
  Slika: string;
  DatumDodavanja: string;
}
@Component({
  selector: 'app-korpa-upravljanje',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatIconModule],
  templateUrl: './korpa-upravljanje.component.html',
  styleUrls: ['./korpa-upravljanje.component.css'],
})
export class KorpaUpravljanjeComponent implements OnInit {
  korpaItems: KorpaItem[] = [];
  constructor(
    private http: HttpClient,
    public MyAuth: MyAuthService,
    private dialog: MatDialog,
    private router: Router,
    private dialogService:DialogServiceService
  ) {}

  ngOnInit() {
    this.fetchKorpaItems();
    this.fetchDostavljaci();
  }

  korpa: KorpaGetByIDResponseKorpa[] = [];

  private fetchKorpaItems() {
    const korpaUrl =
      MojConfig.server_adresa +
      `/Korpa-GetByID?ID=${this.MyAuth.getAuthorizationToken()?.korisnickiNalogID}`;
    this.http.get<KorpaGetByIDResponse>(korpaUrl).subscribe((data) => {
      this.korpa = data.korpa;
    });
  }

  obrisiIzKorpe(id: number) {
    const url = MojConfig.server_adresa + `/Korpa-Obrisi?ID=${id}`;

    this.http.delete(url).subscribe((res: any) => {
      this.dialogService.openOkDialog("Artikal uspješno obrisan!");
        this.ngOnInit();
    });
  }
  dostavljac:Dostavljac[]=[];
  fetchDostavljaci() {
    let url = MojConfig.server_adresa + `/Dostavljaci-GetAll`;
    this.http.get<DostavljacGetAllResponse>(url).subscribe((data) => {
      this.dostavljac = data.dostavljac;
    });
  }



  narudzbaAktivna: boolean = false;
  ime: string = '';
  prezime: string = '';
  adresa: string = '';
  brojTelefona: string = '';
  odabraniDostavljac: string = 'posta';
  ukupnaCijena: number = 0;

  otvoriNarudzbu() {
    if (this.korpa.length === 0) {
      this.dialogService.openOkDialog('Dodajte artikal u korpu kako biste mogli naručiti!');
    } else {
      this.narudzbaAktivna = true;
      this.izracunajUkupnuCijenu();
    }
  }

  zatvoriNarudzbu() {
    this.narudzbaAktivna = false;
  }

  provjeriUnosIPotvrdi() {
    if (!this.ime || !this.prezime || !this.adresa || !this.brojTelefona) {
      this.dialogService.openOkDialog('Molimo vas da unesete sve potrebne informacije!');
      return;
    }
    if (!this.jePrvoVelikoSlovo(this.ime) || !this.jePrvoVelikoSlovo(this.prezime)) {
      this.dialogService.openOkDialog('Ime i prezime trebaju počinjati velikim slovom!');
      return;
    }
    if (!this.jeValidanBrojTelefona(this.brojTelefona)) {
     this.dialogService.openOkDialog('Broj telefona koji ste unijeli ne ispunjava uslove!');
      return;
    }
    this.potvrdiNarudzbu();
  }

  jePrvoVelikoSlovo(tekst: string): boolean {
    const prvoSlovoRegex = /^[A-Z]/;
    return prvoSlovoRegex.test(tekst);
  }

  jeValidanBrojTelefona(broj: string): boolean {
    const brojTelefonaRegex = /^\d{9,}$/;
    return brojTelefonaRegex.test(broj);
  }

  potvrdiNarudzbu() {
    const narudzbaData = {
      ime: this.ime,
      prezime: this.prezime,
      adresa: this.adresa,
      brojTelefona: this.brojTelefona,
      dostavljac: this.odabraniDostavljac,
      ukupnaCijena: this.ukupnaCijena,
      stavkeNarudzbe: this.korpa.map((item) => ({
        artikalID: item.artikalID,
      })),
    };
    const narudzbaUrl = MojConfig.server_adresa + '/Narudzba-Dodaj';

    this.http.post(narudzbaUrl, narudzbaData).subscribe((response: any) => {
      this.zatvoriNarudzbu();
      this.dialogService.openOkDialog('Narudžba je uspješna!');
      this.obrisiArtikleIzKorpe();
    });
  }

  izracunajUkupnuCijenu() {
    this.ukupnaCijena = this.korpa.reduce(
      (total, item) => total + item.cijena,
      0
    );
  }

  private obrisiArtikleIzKorpe() {
    this.korpa.forEach((item) => {
      const url = MojConfig.server_adresa + `/Korpa-Obrisi?ID=${item.artikalID}`;
      this.http.delete(url).subscribe(
        (res: any) => {
          this.dialogService.openOkDialog('Artikal uspješno obrisan iz korpe!');
        },
        (error) => {
          console.error('Greška prilikom brisanja artikla iz korpe', error);
        }
      );
    });
    this.korpa = [];
  }

  protected readonly MojConfig = MojConfig;
}
