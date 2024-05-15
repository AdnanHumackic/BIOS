import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {AutentifikacijaToken} from "../helper/auth/autentifikacijaToken";

@Injectable({providedIn: 'root'})
export class MyAuthService{
  constructor() {
  }




  getAuthorizationToken():AutentifikacijaToken | null {
    let tokenString = window.localStorage.getItem("my-auth-token")??"";
    try {
      return JSON.parse(tokenString);
    }
    catch (e){
      return null;
    }
  }

  isLogiran():boolean{
    return this.getAuthorizationToken() != null;
  }

  isAdmin():boolean {
    return this.getAuthorizationToken()?.korisnickiNalog.isAdmin ?? false
  }
  isRadnik():boolean{
    return this.getAuthorizationToken()?.korisnickiNalog.isRadnik ?? false
  }

  isKupac():boolean{
    return this.getAuthorizationToken()?.korisnickiNalog.isKupac ?? false
  }

  setLogiraniKorisnik(x: AutentifikacijaToken | null) {

    if (x == null){
      window.localStorage.setItem("my-auth-token", '');
    }
    else {
      window.localStorage.setItem("my-auth-token", JSON.stringify(x));
    }
  }


}
