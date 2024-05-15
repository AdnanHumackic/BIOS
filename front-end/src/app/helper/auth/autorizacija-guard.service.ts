import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {MyAuthService} from "../../shared-services/MyAuthService";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AutorizacijaGuardService implements CanActivate {


  constructor(private router: Router, private myAuthService: MyAuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.myAuthService.isLogiran()) {
      let isAdmin = this.myAuthService.isAdmin();
      if (!isAdmin) {
        this.router.navigate(['/error404']);
        return false;
      }

      return true;
    }


    this.router.navigate(['/error404'], {queryParams: {povratniUrl: state.url}});
    return false;
  }

}
