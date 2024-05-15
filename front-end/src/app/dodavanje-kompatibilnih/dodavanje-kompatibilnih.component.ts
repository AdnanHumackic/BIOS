import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatDialogActions, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import {RouterLink} from "@angular/router";
import {MatIconModule} from "@angular/material/icon";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {MojConfig} from "../moj-config";
import {ArtikliGetAllResponse, ArtikliGetAllResponseArtikli} from "./artikal-get-all";
import {KompatibilnostGetAllResponse, KompatibilnostGetAllResponseKompatibilnost} from "./kompatibilnost-get-all";
import {ZadnjeKreirani} from "./get-zadnje-kreirani-artikal";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-dodavanje-kompatibilnih',
  standalone: true,
  imports: [CommonModule, MatDialogActions, MatButtonModule, MatDialogClose, RouterLink, MatDialogContent, MatIconModule, FormsModule, HttpClientModule],
  templateUrl: './dodavanje-kompatibilnih.component.html',
  styleUrl: './dodavanje-kompatibilnih.component.css'
})
export class DodavanjeKompatibilnihComponent implements OnInit{
  constructor(private httpClient:HttpClient, private dialogService:DialogServiceService) {
  }
  dohvatanjeArt:ArtikliGetAllResponseArtikli[]=[];

  ngOnInit(): void {
    let urlArtikala=MojConfig.server_adresa+`/Artikal-GetAll`;
    this.httpClient.get<ArtikliGetAllResponse>(urlArtikala).subscribe((x:ArtikliGetAllResponse)=>{
      this.dohvatanjeArt=x.artikli;
    })

    this.getZadnjeKreirani();
  }
  getZadnjeKreirani() {
    let url=MojConfig.server_adresa+`/Artikal-GetZadnjeKreirani`
    this.httpClient.get<ZadnjeKreirani>(url).subscribe((x:ZadnjeKreirani)=>{
      this.zadnjeKreirani=x;
      /*console.log(this.zadnjeKreirani)*/
      this.artikal1T=this.zadnjeKreirani.id;
    })
  }
  artikal1T!:number;

  promijeniArt($event: Event) {
    // @ts-ignore
    console.log($event.target.value);
  }

  kompatibilnost:KompatibilnostGetAllResponseKompatibilnost[]=[];
  getSveKompatibilnost() {
    let url=MojConfig.server_adresa+`/Kompatibilnost-GetAll`;
    this.httpClient.get<KompatibilnostGetAllResponse>(url).subscribe((x:KompatibilnostGetAllResponse)=>{
      this.kompatibilnost=x.kompatibilnost;
      console.log(this.kompatibilnost);
    })
  }


  zadnjeKreirani!:ZadnjeKreirani;
  kreirajKompatibilnost(kompatibilnost: {artikal1:number,artikal2:number}) {
    kompatibilnost.artikal1=this.artikal1T;
    let url=MojConfig.server_adresa+`/Kompatibilnost-Dodaj`;

    let sveKomp=this.kompatibilnost;

    var postoji:boolean=false;
    sveKomp.forEach((x)=>{
      if(x.art1ID==kompatibilnost.artikal1 && x.art2ID==kompatibilnost.artikal2){
        postoji=true;
      }
    })
    if(postoji==false){
      this.httpClient.post(url, kompatibilnost).subscribe((res)=>{
      })
    }
    else{
      this.dialogService.openOkDialog("Odabrani artikal je već dodan!")
    }


  }

  refresh() {
    this.dialogService.openOkDialog("Uspješno dodan artikal!")
    window.location.reload();
  }
}
