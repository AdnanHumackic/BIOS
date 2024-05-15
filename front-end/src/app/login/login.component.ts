import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from "@angular/forms";
import {AuthLoginRequest} from "./authLoginRequest";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {Router} from "@angular/router";
import {MojConfig} from "../moj-config";
import {AuthLoginResponse} from "./authLoginResponse";
import {MyAuthService} from "../shared-services/MyAuthService";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {log} from "@angular-devkit/build-angular/src/builders/ssr-dev-server";
import {KorisnickiNalogGetbyID} from "../nav-bar/korisnickiNalog-getbyID";
import {yearsPerPage} from "@angular/material/datepicker";
import {SignalRService} from "../shared-services/signalR.service";


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

  public loginRequest: AuthLoginRequest = {
    lozinka:"",
    korisnickoIme:"",
    signalRConnectionID:"",

  };
  constructor(
    public httpClient:HttpClient, private router: Router, public myAuthService:MyAuthService,
    public dialogService:DialogServiceService) { }
  ngOnInit(): void {
  }

  signIn() {
    let url=MojConfig.server_adresa+`/Auth/Login`;
    this.loginRequest.signalRConnectionID=SignalRService.ConnectionID;
    this.httpClient.post<AuthLoginResponse>(url, this.loginRequest).subscribe((x)=>{
      if (!x.isLogiran){
        this.dialogService.openOkDialog("Pogrešno korisničko ime/lozinka!");
      }
      else{
        this.myAuthService.setLogiraniKorisnik(x.autentifikacijaToken);
        this.dialogService.openOkDialog("Prijava je uspješna!").afterClosed().subscribe(res => {
          if (res == true) {
            this.router.navigate(['/oNama']);
            //this.dohvati();
          }
        });
      }
    })
  }


  registracija() {
    this.router.navigate(['/registracija']);
  }
}
