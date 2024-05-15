import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {MyAuthService} from "../../shared-services/MyAuthService";
import {Observable} from "rxjs";
import {isClassReferenceArray} from "@angular/compiler-cli/src/ngtsc/annotations/common";

@Injectable({
  providedIn: 'root'
})
export class AutorizacijaGuardRadnik implements CanActivate {


  constructor(private router: Router, private myAuthService: MyAuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.myAuthService.isLogiran()) {
      let isRadnik = this.myAuthService.isRadnik();
      if (!isRadnik) {
        this.router.navigate(['/error404']);
        return false;
      }

      return true;
    }


    this.router.navigate(['/error404'], {queryParams: {povratniUrl: state.url}});
    return false;
  }

}
