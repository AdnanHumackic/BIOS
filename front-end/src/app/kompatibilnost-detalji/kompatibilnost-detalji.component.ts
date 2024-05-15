import {Component, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ArtikalGetByID} from "../pregled-detalja/artikal-get-by-id";
import {MojConfig} from "../moj-config";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";


@Component({
  selector: 'app-kompatibilnost-detalji',
  standalone: true,
  imports: [CommonModule, MatDialogContent, MatButtonModule, MatIconModule, MatDialogClose, HttpClientModule],
  templateUrl: './kompatibilnost-detalji.component.html',
  styleUrl: './kompatibilnost-detalji.component.css'
})
export class KompatibilnostDetaljiComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data:any, private httpClient:HttpClient) {
  }

  artikal!:ArtikalGetByID;
  ngOnInit(): void {
    this.ucitajDetalje(this.data.vrijednost);
  }

  private ucitajDetalje(vrijednost:number) {
    let url=MojConfig.server_adresa+`/Artikal-GetByID?ID=${vrijednost}`;
    this.httpClient.get<ArtikalGetByID>(url).subscribe((x)=>{
      this.artikal=x;
      console.log(x);
    });
  }
}
