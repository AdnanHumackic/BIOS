import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NarudzbaGetByIDResponse, NarudzbaGetByIdResponseNarudzba } from "./narudzba-GetByID";
import { HttpClient } from "@angular/common/http";
import { MatDialog } from "@angular/material/dialog";
import { MojConfig } from "../moj-config";
import { MyAuthService } from "../shared-services/MyAuthService";
import { DialogServiceService } from "../shared-services/dialog-service.service";
import {DataItemNarudzba, NarudzbaResponse} from "./narudzba-get-by-id-paged";
import {DostavljacGetAllResponse} from "../korpa-upravljanje/dostavljaci-GetAll";

@Component({
  selector: 'app-narudzba',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './narudzba.component.html',
  styleUrl: './narudzba.component.css'
})
export class NarudzbaComponent implements OnInit {
  constructor(private http: HttpClient, private dialog: MatDialog,
              public MyAuth: MyAuthService, private dialogService: DialogServiceService) { }

  ngOnInit() {
    this.loadArtikliPaged(this.currentPage);
  }
  narudzba: DataItemNarudzba[] = [];
  currentPage: number = 1;
  totalPages: number = 1;

  loadArtikliPaged(pageNumber: number) {

    const artPoStranici = 3;
    const urlArt2 = MojConfig.server_adresa + `/Narudzba-GetByIDPaged?ID=${this.MyAuth.getAuthorizationToken()?.korisnickiNalogID}&PageSize=${artPoStranici}&PageNumber=${pageNumber}`;

    this.http.get<NarudzbaResponse>(urlArt2).subscribe((x: NarudzbaResponse) => {
      this.narudzba = x.narudzba.dataItems;
      this.currentPage = x.narudzba.currentPage;
      this.totalPages = x.narudzba.totalPages;

      this.provjeriJelPrazna();

    });
  }
  provjeriJelPrazna() {
    if (this.jelTrenutnaPrazna() && this.currentPage > 1) {
      this.goToPage(this.currentPage - 1);
    }
  }
  jelTrenutnaPrazna(): boolean {
    return this.jelPrazna(this.narudzba);
  }
  jelPrazna(art: DataItemNarudzba[]): boolean {
    return art.length === 0;
  }

  goToPage(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.totalPages) {
      this.loadArtikliPaged(pageNumber);
    }
  }
  getPagesArray() {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  potvrdiIobrisiNarudzbu(id: number) {
    this.dialogService.openConfirmDialog('Jeste li sigurni da je narudžba stigla na vašu adresu?').afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.obrisiNarudzbu(id);
      }
    });
  }

  obrisiNarudzbu(id: number) {
    const narudzbaUrl = MojConfig.server_adresa + `/Narudzba-Obrisi?ID=${id}`;
    this.http.delete(narudzbaUrl).subscribe(() => {
      this.dialogService.openOkDialog('Narudžba uspješno pristigla!');
      this.ngOnInit();
    });
  }
}
