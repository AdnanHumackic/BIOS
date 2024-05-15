import {Component, Inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {ArtikalGetByID} from '../pregled-detalja/artikal-get-by-id';
import {MojConfig} from "../moj-config";
import {
  ArtikalGetAllOsimProslijedjenogResponse,
  ArtikalGetAllOsimProslijedjenogResponseArtikal
} from "./get-all-osim-proslijedjenog";
import {
  KompatibilnostGetAllResponse,
  KompatibilnostGetAllResponseKompatibilnost
} from '../dodavanje-kompatibilnih/kompatibilnost-get-all';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors
} from "@angular/forms";

@Component({
  selector: 'app-update-kompatibilnih',
  standalone: true,
  imports: [CommonModule, MatDialogContent, MatButtonModule, MatIconModule, MatDialogActions, MatDialogClose, RouterLink, FormsModule, HttpClientModule, ReactiveFormsModule],
  templateUrl: './update-kompatibilnih.component.html',
  styleUrl: './update-kompatibilnih.component.css'
})
export class UpdateKompatibilnihComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, public httpClient: HttpClient, private dialogService: DialogServiceService) {
  }


  kompatibilnost: KompatibilnostGetAllResponseKompatibilnost[] = [];

  ngOnInit(): void {
    this.UcitajPodatkeArtikla();
    this.ucitajSveOsimPosljednjeg();
    this.getSveKompatibilnost();
    console.log(this.data.vrijednost);
  }

  artikli: ArtikalGetAllOsimProslijedjenogResponseArtikal[] = [];
  kompatibilnostUpdate = new FormGroup({
    artikal1: new FormControl(''),
    artikal2: new FormControl('', this.provjeriPostojanje.bind(this))
  });

  getSveKompatibilnost() {
    let url = MojConfig.server_adresa + `/Kompatibilnost-GetAll`;
    this.httpClient.get<KompatibilnostGetAllResponse>(url).subscribe((x: KompatibilnostGetAllResponse) => {
      this.kompatibilnost = x.kompatibilnost;
    })
  }

  provjeriPostojanje(control: AbstractControl): ValidationErrors | null {
    const artikal1ID = this.data.vrijednost;
    const artikal2ID = control.value;

    var postoji: boolean = false;
    let sveKomp = this.kompatibilnost;
    sveKomp.forEach((x) => {
      if (x.art1ID == artikal1ID && x.art2ID == artikal2ID) {
        postoji = true;
      }
    })


    if (postoji) {
      return {kompatibilnostPostoji: true};
    } else {
      return null;
    }
  }


  kreirajKompatibilnost() {
    /*kombinacija.artikal1 = this.data.vrijednost;

    let sveKomp = this.kompatibilnost;
    var postoji: boolean = false;
    let url = MojConfig.server_adresa + `/Kompatibilnost-Dodaj`;

    sveKomp.forEach((x) => {
      if (x.art1ID == kombinacija.artikal1 && x.art2ID == kombinacija.artikal2) {
        postoji = true;
      }
    })
    if (postoji == false) {
      this.httpClient.post(url, kombinacija).subscribe((res) => {
      })
    } else {
      this.dialogService.openOkDialog("Odabrani artikal je već dodan!")
    }*/
    const formData = {
      artikal1: this.data.vrijednost,
      artikal2: this.kompatibilnostUpdate.get('artikal2')?.value
    }
    if (this.kompatibilnostUpdate.valid) {

      let url = MojConfig.server_adresa + `/Kompatibilnost-Dodaj`;
      this.httpClient.post(url, formData).subscribe((res) => {
      })
      this.kompatibilnostUpdate.reset();

    }
  }

  ucitajSveOsimPosljednjeg() {
    let url = MojConfig.server_adresa + `/Artikal-GetAllOsimProslijednjenog?ID=${this.data.vrijednost}`;

    this.httpClient.get<ArtikalGetAllOsimProslijedjenogResponse>(url).subscribe((x: ArtikalGetAllOsimProslijedjenogResponse) => {
      this.artikli = x.artikal;
    })
  }

  public artikalGetByID!: ArtikalGetByID;

  UcitajPodatkeArtikla() {
    let url = MojConfig.server_adresa + `/Artikal-GetByID?ID=${this.data.vrijednost}`;
    this.httpClient.get<ArtikalGetByID>(url).subscribe((x: ArtikalGetByID) => {
      this.artikalGetByID = x;
      console.log(x);
    })
  }


  refresh() {
    this.dialogService.openOkDialog("Slijedi reload stranice!").afterClosed().subscribe((x=>{
      window.location.reload();
   }))
    //ne reaguje kako treba
  }
}
