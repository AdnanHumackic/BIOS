import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {MyAuthService} from "../../shared-services/MyAuthService";
import {tap} from "rxjs";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class MyAuthInterceptorService implements HttpInterceptor {

  constructor(private auth:MyAuthService, private router: Router) { }
//ovdje je belaj
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    // Get the auth token from the service.
    const authToken = this.auth.getAuthorizationToken()?.vrijednost??"";
    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    const authReq = req.clone({
      headers: req.headers.set('my-auth-token', authToken)
    });

    // send cloned request with header to the next handler.
    return next.handle(authReq).pipe(
      tap(()=>{}, err=>{
        if (err instanceof HttpErrorResponse)
        {
          if (err.status !== 401){
            return;
          }

          this.router.navigateByUrl('/login');
        }
      })
    );
  }
}
