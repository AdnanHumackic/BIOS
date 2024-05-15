import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {catchError, map, of} from "rxjs";


@Injectable({
  providedIn:'any'
})

export class IdValidanService{
  constructor(private httpClient:HttpClient, private router:Router) {
  }

  provjeriId(id:any){
    let url=MojConfig.server_adresa+`/Artikal-GetByID?ID=${id}`
    return this.httpClient.get<any>(url).pipe(map(response=>{
      return true;
    }),
      catchError(err=>{
        this.router.navigate(['/error404']);
        return of(false);
      })
    )
  }
}
