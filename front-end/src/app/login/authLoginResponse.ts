import {AutentifikacijaToken} from "../helper/auth//autentifikacijaToken";

export interface AuthLoginResponse {
  autentifikacijaToken: AutentifikacijaToken
  isLogiran: boolean
}


//ne salje se requst za login uopste
