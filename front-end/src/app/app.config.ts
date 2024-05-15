import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {MyAuthInterceptorService} from "./helper/auth/my-auth-interceptor.service";
import {AutorizacijaGuardService} from "./helper/auth/autorizacija-guard.service";
import {AutorizacijaGuardRadnik} from "./helper/auth/autorizacija-guard-radnik";
import {AutorizacijaGuardKupac} from "./helper/auth/autorizacija-guard-kupac";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAnimations(),
    { provide: HTTP_INTERCEPTORS, useClass: MyAuthInterceptorService, multi: true },
    AutorizacijaGuardService, AutorizacijaGuardRadnik, AutorizacijaGuardKupac]
};
