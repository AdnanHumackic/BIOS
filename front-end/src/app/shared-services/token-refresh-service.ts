import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {interval, takeWhile} from 'rxjs';
import { switchMap } from 'rxjs/operators';
import {MyAuthService} from "./MyAuthService";
import {MojConfig} from "../moj-config";
import {AutentifikacijaToken} from "../helper/auth/autentifikacijaToken";

@Injectable({
  providedIn: 'any'
})
export class TokenRefreshService {

  constructor(private http: HttpClient, private authService: MyAuthService) { }

  startTokenRefreshInterval(oldToken: string) {
    this.refreshAuthToken(oldToken);

    interval(30 * 1000).pipe(
      takeWhile(() => this.authService.isLogiran()),
      switchMap(() => this.refreshAuthToken(oldToken))
    ).subscribe();
  }


  refreshAuthToken(oldToken: string) {
    let url = MojConfig.server_adresa + `/JWT/Provjeri`;
    let headers = {
      'my-auth-token': oldToken
    };
    return this.http.post<any>(url, { oldToken: oldToken }, { headers: headers }).pipe(
      switchMap(resp => {
        if (resp && resp.noviToken) {

          let newToken: AutentifikacijaToken = {
            id: resp.autentifikacijaToken.id,
            vrijednost: resp.noviToken,
            korisnickiNalogID: resp.autentifikacijaToken.korisnickiNalogID,
            korisnickiNalog: resp.autentifikacijaToken.korisnickiNalog,
            vrijemeEvidentiranja: resp.autentifikacijaToken.vrijemeEvidentiranja,
            ipAdresa: resp.autentifikacijaToken.ipAdresa,
            is2FOtkljucano: resp.autentifikacijaToken.is2FOtkljucano,
          };
          // @ts-ignore
          this.authService.setLogiraniKorisnik(newToken);
          window.localStorage.setItem("my-auth-token", JSON.stringify(newToken));

          return resp;
        }
        else{
          return resp;}
      })
    );
  }

}
