import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyAuthService } from "../shared-services/MyAuthService";
import { RouterOutlet } from "@angular/router";
import { StavkeRadnikResponse, StavkeRadnikResponseStavke } from "./radnik-GetAll";
import { HttpClient, HttpClientModule } from "@angular/common/http";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { MojConfig } from "../moj-config";
import { StavkeDrzavaResponse } from "../registracija/drzave-GetAll";
import { Subject } from 'rxjs';
import {StavkeZadatakResponse, StavkeZadatakResponseStavke} from "./zadaciGetAll";
import {ZadaciGetByIdResponse} from "./Zadaci-GetById";
import {DialogServiceService} from "../shared-services/dialog-service.service";

export interface ZadaciRequest {
  id: number;
  naziv: string;
  opis: string;
  datumPocetka: string;
  datumZavrsetka: string;
  radnikId: number;
}

@Component({
  selector: 'app-zadaci',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ReactiveFormsModule],
  templateUrl: './zadaci.component.html',
  styleUrl: './zadaci.component.css'
})
export class ZadaciComponent implements OnInit {
  zadaciRequest: ZadaciRequest = {
    id: 0,
    naziv: '',
    opis: '',
    datumPocetka: '',
    datumZavrsetka: '',
    radnikId: 0
  };

  radnik: StavkeRadnikResponseStavke[] = [];
  // @ts-ignore
  zadaciForm: FormGroup;
  zadaci: StavkeZadatakResponseStavke[] = [];
  private refreshZadaci$ = new Subject<void>();

  constructor(public myAuth: MyAuthService,
              private http: HttpClient,
              private fb: FormBuilder,
              private dialogService:DialogServiceService) {}

  ngOnInit() {
    this.createForm();
    this.fetchRadnike();
    this.fetchZadaci();
    this.refreshZadaci$.subscribe(() => {
      this.fetchZadaci();
    });
  }

  private createForm() {
    this.zadaciForm = this.fb.group({
      naziv: ['', Validators.compose([Validators.required])],
      opis: ['', Validators.compose([Validators.required])],
      datumPocetka: ['', Validators.compose([Validators.required])],
      datumZavrsetka: ['', Validators.compose([Validators.required, this.datumValidator()])],
      radnikId: [0, Validators.required]
    });
  }

  private fetchRadnike() {
    const radnikUrl = MojConfig.server_adresa + `/Radnik-GetAll`;
    this.http.get<StavkeRadnikResponse>(radnikUrl).subscribe((data) => {
      this.radnik = data.radnik;
    });
  }

  private fetchZadaci() {
    if (this.myAuth.isAdmin()) {
      const zadaciUrl = MojConfig.server_adresa + `/Zadaci-GetAll`;
      this.http.get<StavkeZadatakResponse>(zadaciUrl).subscribe((data) => {
        this.zadaci = data.stavkeZadatak;
      });
    } else if (this.myAuth.isRadnik()) {
      const zadatakUrl = MojConfig.server_adresa + `/Zadaci-GetByID?ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalogID}`;
      this.http.get<ZadaciGetByIdResponse>(zadatakUrl).subscribe((data) => {
        this.zadaci = data.zadaci;
      });
    }
  }


  Dodaj() {
    if (this.zadaciForm.invalid) {
        this.dialogService.openOkDialog("Molimo unesite sve potrebne informacije!");
      return;
    }
    this.zadaciRequest = this.zadaciForm.value;
    const zadaciUrl = MojConfig.server_adresa + `/Zadatak-Dodaj`;

    this.http.post(zadaciUrl, this.zadaciRequest).subscribe((response: any): void => {
      this.dialogService.openOkDialog("Dodavanje je uspjesno!");
      this.refreshZadaci$.next();
      this.zadaciForm.reset();
    });
  }

  private datumValidator() {
    return (control: { value: string }) => {
      const inputDate = new Date(control.value);
      const currentDate = new Date();
      return inputDate >= currentDate ? null : { dateInFuture: true };
    };
  }

  protected readonly MyAuthService = MyAuthService;

    Obrisi(id: number) {

        const ZadUrl=MojConfig.server_adresa+`/Zadaci-Obrisi?ID=${id}`;
        this.http.delete(ZadUrl).subscribe((res:any):void=>{
          this.dialogService.openOkDialog("Zadatak uspjesno odradjen!");
          this.ngOnInit();
        })
    }
}
