import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import { MojConfig } from "../moj-config";
import { StavkeDrzavaResponse, StavkeDrzavaResponseStavke } from "./drzave-GetAll";
import {DialogServiceService} from "../shared-services/dialog-service.service";

export interface RegistrationRequest {
  id: number;
  korisnickoIme: string;
  ime: string;
  prezime: string;
  email: string;
  lozinka: string;
  drzava: number;
  datumRodjenja: string;
}

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {
  registrationRequest: RegistrationRequest = {
    id: 0,
    korisnickoIme: '',
    ime: '',
    prezime: '',
    email: '',
    lozinka: '',
    drzava: 0,
    datumRodjenja: ''
  };

  drzava: StavkeDrzavaResponseStavke[] = [];
  // @ts-ignore
  registrationForm: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder,
              private dialogService:DialogServiceService) {}

  ngOnInit() {
    this.createForm();
    this.fetchDrzave();
  }

  private createForm() {
    this.registrationForm = this.fb.group({
      korisnickoIme: ['', Validators.compose([Validators.required, Validators.pattern(/^[a-zA-Z][a-zA-Z0-9_]*$/)])],
      ime: ['', Validators.compose([Validators.required, Validators.pattern(/^[A-Z][a-zA-Z]*$/)])],
      prezime: ['', Validators.compose([Validators.required, Validators.pattern(/^[A-Z][a-zA-Z]*$/)])],
      email: ['', Validators.compose([Validators.required, Validators.pattern(/^[a-zA-Z0-9]+@[a-zA-Z]+\.[a-zA-Z]+$/)])],
      lozinka: ['', Validators.required],
      drzava: [1, Validators.required],
      datumRodjenja: ['', Validators.compose([Validators.required, this.datumValidator()])]
    });
  }

  private datumValidator() {
    return (control: { value: string }) => {
      const inputDate = new Date(control.value);
      const currentDate = new Date();
      return inputDate <= currentDate ? null : { dateInFuture: true };
    };
  }
  private fetchDrzave() {
    const drzavaUrl = MojConfig.server_adresa + `/Drzava-GetAll`;
    this.http.get<StavkeDrzavaResponse>(drzavaUrl).subscribe((data) => {
      this.drzava = data.drzava;
    });
  }

  registrirajSe() {
    if (this.registrationForm.invalid) {
      this.dialogService.openOkDialog("Molimo vas da unesete sve potrebne informacije!");
      return;
    }

    this.registrationRequest = this.registrationForm.value;
    const registracijaUrl = MojConfig.server_adresa + `/Kupac-Dodaj`;

    this.http.post(registracijaUrl, this.registrationRequest).subscribe((response: any) => {
      this.dialogService.openOkDialog("Registracija je uspješna!");
    });
  }
}

@NgModule({
  declarations: [RegistracijaComponent],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule],
})
export class RegistracijaModule {}
