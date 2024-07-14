import {RouterModule, Routes} from '@angular/router';
import {PregledArtikalaComponent} from "./pregled-artikala/pregled-artikala.component";
import {PregledDetaljaComponent} from "./pregled-detalja/pregled-detalja.component";
import {DodavanjeArtiklaComponent} from "./dodavanje-artikla/dodavanje-artikla.component";
import {UpdateArtiklaComponent} from "./update-artikla/update-artikla.component";
import {UpravljanjeArtiklimaComponent} from "./upravljanje-artiklima/upravljanje-artiklima.component";
import{WishlistUpravljanjeComponent} from "./wishlist-upravljanje/wishlist-upravljanje.component";
import {KorpaUpravljanjeComponent} from "./korpa-upravljanje/korpa-upravljanje.component";
import {ONamaComponent} from "./o-nama/o-nama.component";
import {LoginComponent} from "./login/login.component";
import {AutorizacijaGuardService} from "./helper/auth/autorizacija-guard.service";
import {NarudzbaComponent} from "./narudzba/narudzba.component";
import {RegistracijaComponent} from "./registracija/registracija.component";
import {ZadaciComponent} from "./zadaci/zadaci.component";
import {AdminAkcijeComponent} from "./admin-akcije/admin-akcije.component";
import {NeovlastenPristupComponent} from "./neovlasten-pristup/neovlasten-pristup.component";
import {AutorizacijaGuardRadnik} from "./helper/auth/autorizacija-guard-radnik";
import {AutorizacijaGuardKupac} from "./helper/auth/autorizacija-guard-kupac";

export const routes: Routes = [
  {path: '', component: ONamaComponent, pathMatch: 'full' },
  {path: 'pregledKomp', component:PregledArtikalaComponent},
  {path: 'detaljanPregled/:id', component:PregledDetaljaComponent},
  {path: 'dodavanjeArt', component: DodavanjeArtiklaComponent, canActivate:[AutorizacijaGuardRadnik]},
  {path: 'updateArt/:id', component: UpdateArtiklaComponent, canActivate:[AutorizacijaGuardRadnik]},
  {path: 'upravljanjeArtiklima', component: UpravljanjeArtiklimaComponent, canActivate:[AutorizacijaGuardRadnik]},
  {path: 'wishlist', component:WishlistUpravljanjeComponent, canActivate:[AutorizacijaGuardKupac]},
  {path: 'korpa', component:KorpaUpravljanjeComponent, canActivate:[AutorizacijaGuardKupac]},
  {path: 'homePage', component: ONamaComponent},
  {path: 'login', component: LoginComponent},
  {path: 'narudzba', component: NarudzbaComponent, canActivate:[AutorizacijaGuardKupac]},
  {path: 'registracija', component: RegistracijaComponent},
  {path: 'zadaci', component: ZadaciComponent},
  {path: 'adminAkcije', component: AdminAkcijeComponent, canActivate:[AutorizacijaGuardService]},
  {path: 'error404', component: NeovlastenPristupComponent}




];
