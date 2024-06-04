import {Component, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ArtikalGetByID} from "../pregled-detalja/artikal-get-by-id";
import {MojConfig} from "../moj-config";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {
  KompatibilnostGetByIdResponseKompatibilnost,
  KompGetByIDResponse
} from "../pregled-detalja/kompatibilnost-get-by-id";


@Component({
  selector: 'app-kompatibilnost-detalji',
  standalone: true,
  imports: [CommonModule, MatDialogContent, MatButtonModule, MatIconModule, MatDialogClose, HttpClientModule, FormsModule, ReactiveFormsModule],
  templateUrl: './kompatibilnost-detalji.component.html',
  styleUrl: './kompatibilnost-detalji.component.css'
})
export class KompatibilnostDetaljiComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data:any, private httpClient:HttpClient) {
  }

  artikal!:ArtikalGetByID;
  ngOnInit(): void {
    this.ucitajDetalje(this.data.vrijednost);
    this.ucitajKompatibilne(this.data.vrijednost)
  }

  private ucitajDetalje(vrijednost:number) {
    let url=MojConfig.server_adresa+`/Artikal-GetByID?ID=${vrijednost}`;
    this.httpClient.get<ArtikalGetByID>(url).subscribe((x)=>{
      this.artikal=x;
      console.log(x);
    });
  }
  kompatibilnostGetById: KompatibilnostGetByIdResponseKompatibilnost[] = [];

  ucitajKompatibilne(id: any) {
    let urlKomp = MojConfig.server_adresa + `/Kompatibilnost-GetByID?ID=${id}`;
    this.httpClient.get<KompGetByIDResponse>(urlKomp).subscribe((y: KompGetByIDResponse) => {
      this.kompatibilnostGetById = y.komp;
    })
  }
  currentIndex1: number = 0;
  itemsPerPage: number = 4;
  next() {
    if (this.currentIndex1 + this.itemsPerPage < this.kompatibilnostGetById.length) {
      this.currentIndex1 += this.itemsPerPage;
    }
  }

  prev() {
    if (this.currentIndex1 - this.itemsPerPage >= 0) {
      this.currentIndex1 -= this.itemsPerPage;
    }
  }

  getVisibleItems() {
    return this.kompatibilnostGetById.slice(this.currentIndex1, this.currentIndex1 + this.itemsPerPage);
  }
  protected readonly MojConfig = MojConfig;
}
