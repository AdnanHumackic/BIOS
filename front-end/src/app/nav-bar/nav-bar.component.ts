import {Component, OnInit, ViewChild, ElementRef, Output, EventEmitter} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Router, RouterLink, RouterOutlet} from "@angular/router";
import {MyAuthService} from "../shared-services/MyAuthService";
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {KorisnickiNalogGetbyID} from "./korisnickiNalog-getbyID";
import {UpravljanjeArtiklimaComponent} from "../upravljanje-artiklima/upravljanje-artiklima.component";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DodavanjeArtiklaComponent} from "../dodavanje-artikla/dodavanje-artikla.component";
import {KupacProfilEditComponent} from "../kupac-profil-edit/kupac-profil-edit.component";
import {DodavanjeRadnikaComponent} from "../dodavanje-radnika/dodavanje-radnika.component";
import {RadnikPromijeniLozinkuComponent} from "../radnik-promijeni-lozinku/radnik-promijeni-lozinku.component";
import {empty} from "rxjs";
import {SignalRService} from "../shared-services/signalR.service";


@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, HttpClientModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit{



  /*korisnik:KorisnickiNalogGetbyID | null=null;
  dohvati(){
    let url=MojConfig.server_adresa + `/Korisnik-GetByID?ID=${this.myAuthService.getAuthorizationToken()?.korisnickiNalogID}`
    this.httpClient.get<KorisnickiNalogGetbyID>(url).subscribe(x => {
      this.korisnik = x;
      console.log(this.korisnik);

    })
  }*/

  constructor(public router:Router, public myAuthService:MyAuthService, private httpClient:HttpClient,
              public dialogService: DialogServiceService, public dialog:MatDialog) {
  }

  ngOnInit(): void {
    /*if(this.myAuthService.isLogiran()){
      this.dohvati();
    }*/
  }

  logout() {
    let token = window.localStorage.getItem("my-auth-token")??"";

    window.localStorage.setItem("my-auth-token","");
    let url=MojConfig.server_adresa+`/Auth/Logout`
    this.httpClient.post(url, {
      signalRConnectionID:SignalRService.ConnectionID
    }, {
      headers:{
        "my-auth-token": token
      }
    }).subscribe(x=>{
      this.dialogService.openOkDialog("Odjava uspješna!").afterClosed().subscribe(x=>{
        if(x==true){
          this.router.navigate(['/oNama']);
        }
      });
    })

  }



  protected readonly UpravljanjeArtiklimaComponent = UpravljanjeArtiklimaComponent;
  refreshTimestamp: number = Date.now();
  otvoriDijalog() {
    const dialogConfig= new MatDialogConfig();
    dialogConfig.disableClose=true;
    dialogConfig.autoFocus=true;
    dialogConfig.width="100%";
    const dialogRef=this.dialog.open(KupacProfilEditComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((data: any) => {
      this.refreshTimestamp = Date.now();
    });
  }

  dodavanjeDijalog() {
    const dialogConfig= new MatDialogConfig();
    dialogConfig.disableClose=true;
    dialogConfig.autoFocus=true;
    dialogConfig.width="100%";
    const dialogRef=this.dialog.open(DodavanjeRadnikaComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((data: any) => {
      this.refreshTimestamp = Date.now();
    });
  }

  promijeniLozinkuDijalog() {
    const dialogConfig= new MatDialogConfig();
    dialogConfig.disableClose=true;
    dialogConfig.autoFocus=true;
    dialogConfig.width="100%";
    const dialogRef=this.dialog.open(RadnikPromijeniLozinkuComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((data: any) => {
      this.refreshTimestamp = Date.now();
    });
  }

  protected readonly MojConfig = MojConfig;


}
