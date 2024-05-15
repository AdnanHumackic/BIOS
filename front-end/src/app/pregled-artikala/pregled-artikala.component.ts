import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterLink} from "@angular/router";
import {ArtikalPretragaResponse, ArtikalPretragaResponseArtikal} from "./artikli-pretraga-response";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {SharedServiceService} from "../shared-services/shared-service.service";
import {StavkeWishlistResponse, StavkeWishlistResponseStavke} from "../wishlist-upravljanje/wishlist-GetAll";
import {StavkeKorpaResponse, StavkeKorpaResponseStavke} from "../korpa-upravljanje/korpa-GetAll";
import {TipGetAllResponse, TipGetAllResponseTip} from '../dodavanje-artikla/tip-get-all';
import {FormsModule} from "@angular/forms";
import {MatSliderModule} from "@angular/material/slider";
import {MyAuthService} from "../shared-services/MyAuthService";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {ArtikalPretraga, DataItemPretraga} from "./artikli-pretraga-paged-response";
import {Observable} from "rxjs";


@Component({
  selector: 'app-pregled-artikala',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, MatSliderModule],
  templateUrl: './pregled-artikala.component.html',
  styleUrl: './pregled-artikala.component.css'
})
export class PregledArtikalaComponent implements OnInit {

  constructor(public httpClient: HttpClient, public sharedService: SharedServiceService,
              public myAuthService: MyAuthService,
              private dialogService: DialogServiceService) {
  }


  tip: TipGetAllResponseTip[] = [];

  ngOnInit(): void {

    let urlTip = MojConfig.server_adresa + `/TipArtikla-GetAll`;
    this.httpClient.get<TipGetAllResponse>(urlTip).subscribe((x: TipGetAllResponse) => {
      this.tip = x.tip.reverse();
    })
    this.getFiltrirano(this.naziv, this.tipID, this.vrijednostMin, this.vrijednostMax, this.proizvodjac, this.currentPage)
  }

  wishlist: StavkeWishlistResponseStavke[] = [];
  korpa: StavkeKorpaResponseStavke[] = [];

  naziv: string = '';
  tipID: number = 4;
  vrijednostMin = 0;
  vrijednostMax: number = 2000;
  proizvodjac: string='';


  dodajUWishlist(artikal: ArtikalPretragaResponseArtikal) {
    const wishlistUrl = MojConfig.server_adresa + '/Wishlist-Dodaj';
    const wishlistItem = {
      artikalID: artikal.id
    };
    let cijeliWishlist = this.wishlist;
    var postoji = false;
    cijeliWishlist.forEach((x) => {
      if (x.artikalId == artikal.id) {
        postoji = true
      }
    })

    if (!this.myAuthService.isLogiran()) {
      this.dialogService.openOkDialog("Da biste dodali artikal u wishlist, potrebno je da se prijavite na korisnički račun!")
    } else {

      if (postoji == false) {
        this.httpClient.post(wishlistUrl, wishlistItem).subscribe(() => {
          this.dialogService.openOkDialog('Artikal dodan u wishlist!');
          this.OsvjeziWishlist();
        });
      } else {
        this.dialogService.openOkDialog('Artikal vec postoji u wishlistu!');
      }
    }

  }

  OsvjeziWishlist() {
    const wishlistUrl = MojConfig.server_adresa + `/Wishlist-GetAll`;
    this.httpClient.get<StavkeWishlistResponse>(wishlistUrl).subscribe((data) => {
      this.wishlist = data.stavkeWishlist;
    });
  }

  dodajUKorpu(artikal: ArtikalPretragaResponseArtikal) {
    const korpaUrl = MojConfig.server_adresa + '/Korpa-Dodaj';
    const korpaItem = {
      artikalID: artikal.id
    };
    let cijelaKorpa = this.korpa;
    var postoji = false;
    cijelaKorpa.forEach((x) => {
      if (x.artikalID == artikal.id) {
        postoji = true
      }
    })
    if (!this.myAuthService.isLogiran()) {
      this.dialogService.openOkDialog("Da biste dodali artikal u korpu, potrebno je da se prijavite na korisnički račun!")
    } else {
      if (postoji == false) {
        this.httpClient.post(korpaUrl, korpaItem).subscribe(() => {
          this.dialogService.openOkDialog('Artikal dodan u korpu!');
          this.OsvjeziKorpu();
        });
      } else {
        this.dialogService.openOkDialog('Artikal već postoji u korpi!');
      }
    }
  }

  OsvjeziKorpu() {
    const korpaUrl = MojConfig.server_adresa + `/Korpa-GetAll`;
    this.httpClient.get<StavkeKorpaResponse>(korpaUrl).subscribe((data) => {
      this.korpa = data.stavkeKorpa;
    });
  }

  currentPage: number = 1;
  totalPages: number = 1;

  provjeriJelPrazna() {
    if (this.jelTrenutnaPrazna() && this.currentPage > 1) {
      this.goToPage(this.currentPage - 1);
    }
  }

  jelTrenutnaPrazna(): boolean {
    return this.jelPrazna(this.art);
  }

  jelPrazna(art: DataItemPretraga[]): boolean {
    return art.length === 0;
  }

  goToPage(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.totalPages) {
      this.getFiltrirano(this.naziv, this.tipID, this.vrijednostMin, this.vrijednostMax, this.proizvodjac, pageNumber);
    }
  }


  getPagesArray() {
    return Array.from({length: this.totalPages}, (_, i) => i + 1);
  }


  cijena() {
    /*const artPoStranici = 6;
    let url = '';

    if (this.pretrazeni.length > 0) {
      url = MojConfig.server_adresa + `/Artikal-PretragaPaged?Pretraga=${this.naziv}&TipID=${this.tipID}&pageNumber=${this.currentPage}&pageSize=${artPoStranici}`;
    } else {
      url = MojConfig.server_adresa + `/Artikal-PretragaPaged?Pretraga=${this.naziv}&TipID=${this.tipID}&pageNumber=${this.currentPage}&pageSize=${artPoStranici}`;
    }

    this.httpClient.get<ArtikalPretraga>(url).subscribe((x: ArtikalPretraga) => {
      this.filter2 = x.artikal.dataItems.filter((item) => {
        return item.cijena >= this.vrijednostMin && item.cijena <= this.vrijednostMax;
      });

      this.currentPage = x.artikal.currentPage;
      this.totalPages = x.artikal.totalPages;
      this.isVidljivo = true;
    });*/

  }

  ResetujFilter() {
    this.vrijednostMin = 0;
    this.vrijednostMax = 2000;
    this.naziv = '';
    this.tipID = 4;
    this.getFiltrirano(this.naziv, this.tipID, this.vrijednostMin, this.vrijednostMax, this.proizvodjac, this.currentPage)
  }

  protected readonly MojConfig = MojConfig;


  generisiKriptovaniId(id: number) {
    const hashedId = btoa(id.toString());
    return hashedId;
  }

  art: DataItemPretraga[] = [];

  getFiltrirano(naziv: string, tipID: number, vrijednostMin: number, vrijednostMax: number, proizvodjac: string, currentPage: number){
    const pageSize=8;
    if(tipID==4){
      let url=MojConfig.server_adresa+`/Artikal-PretragaPaged?Pretraga=${naziv}&Proizvodjac=${proizvodjac}&cijenaOd=${vrijednostMin}&cijenaDo=${vrijednostMax}&PageSize=${pageSize}&PageNumber=${currentPage}`;
      this.httpClient.get<ArtikalPretraga>(url).subscribe(x=>{
        this.art=x.artikal.dataItems;
        this.currentPage=x.artikal.currentPage;
        this.totalPages=x.artikal.totalPages;
      })
      this.provjeriJelPrazna();
    }
    else{
      let url=MojConfig.server_adresa+`/Artikal-PretragaPaged?Pretraga=${naziv}&TipID=${tipID}&Proizvodjac=${proizvodjac}&cijenaOd=${vrijednostMin}&cijenaDo=${vrijednostMax}&PageSize=${pageSize}&PageNumber=${currentPage}`;
      this.httpClient.get<ArtikalPretraga>(url).subscribe(x=>{
        this.art=x.artikal.dataItems;
        this.currentPage=x.artikal.currentPage;
        this.totalPages=x.artikal.totalPages;
      })
      this.provjeriJelPrazna();
    }

  }


}
