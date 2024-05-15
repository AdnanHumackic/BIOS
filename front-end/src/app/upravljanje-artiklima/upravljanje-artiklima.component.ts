import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import {SharedServiceService} from "../shared-services/shared-service.service";
import {MojConfig} from "../moj-config";
import {ArtikalPretragaResponse, ArtikalPretragaResponseArtikal} from "../pregled-artikala/artikli-pretraga-response";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {RouterLink} from "@angular/router";
import {DodavanjeArtiklaComponent} from "../dodavanje-artikla/dodavanje-artikla.component";
import {MatDialog, MatDialogConfig, MatDialogRef} from "@angular/material/dialog";
import {UpdateArtiklaComponent} from "../update-artikla/update-artikla.component";
import {
  ArtikliGetAllKojiNisuObrisaniResponse,
  ArtikliGetAllKojiNisuObrisaniResponseArtikli
} from "./get-all-koji-nisu-obrisani";
import {MyAuthService} from "../shared-services/MyAuthService";
import {Artikli, ArtikliPaged, DataItem} from "./get-all-koji-nisu-obrisni-paged-response";
import {ArtikliResponse, DataItemObrisani} from "./get-all-koji-su-obrisani-paged-response";
import {PrikaziArtikliZadaci} from "./prikaziArtikliZadaci";
import {StavkeZadatakResponse, StavkeZadatakResponseStavke} from "../zadaci/zadaciGetAll";
import {ZadaciGetByIdResponse} from "../zadaci/Zadaci-GetById";
import {DataItemNarudzbe, Narudzbe} from "./narudzba-get-all-paged";
import {DokumentResponse} from "./DokumentResponse";
import {DokumentiPaged, DokumentiPagedResponse} from "./get-dokumenti-paged";
import {DataItemZadaci, ZadaciPaged} from "./zadaci-get-by-id-paged";
import {DataItemWishlist, WishlistPaged} from "../wishlist-upravljanje/wishlist-get-by-id-paged";

@Component({
  selector: 'app-upravljanje-artiklima',
  standalone: true,
  imports: [CommonModule, RouterLink, HttpClientModule, DodavanjeArtiklaComponent],
  templateUrl: './upravljanje-artiklima.component.html',
  styleUrl: './upravljanje-artiklima.component.css'
})
export class UpravljanjeArtiklimaComponent implements OnInit {

  constructor(public httpClient: HttpClient, public sharedService: SharedServiceService,
              private dialogService: DialogServiceService, private dialog: MatDialog,
              public myAuth: MyAuthService) {
  }

  obrisiArtikal(neobrisani: ArtikliGetAllKojiNisuObrisaniResponseArtikli) {
    const obrItem = {
      artID: neobrisani.id
    }
    this.dialogService.openConfirmDialog('Da li ste sigurni da želite obrisati artikal?')
      .afterClosed().subscribe(res => {
      if (res == true) {

        let urlObr = MojConfig.server_adresa + `/Artikal-DodajUObrisane`
        this.httpClient.post(urlObr, obrItem).subscribe((res: any) => {
          this.ngOnInit();
        })
        this.dialogService.openOkDialog("Artikal uspješno obrisan!");
      }

    });
  }

  artikal: ArtikalPretragaResponseArtikal[] = [];
  zad:DataItemZadaci[]=[];
  trenutnaStranica:number=1;
  ukupnoStranica:number=1;

  art: DataItem[] = [];
  currentPage: number = 1;
  totalPages: number = 1;

  ngOnInit(): void {
    this.loadArtikliPaged(this.currentPage);
    this.loadObrisani(this.currentPage);
    this.loadNarudzbe(this.currentPage);
    this.fetchZadatke();
    this.dohvatiDokumente(this.currentPage);
    this.loadZadaciPaged(this.trenutnaStranica)
  }

  loadZadaciPaged(pageNumber: number){
    const zadPoStranici = 3;
    const urlZad2 = MojConfig.server_adresa+`/Zadaci-GetByIDPaged?PageSize=${zadPoStranici}&PageNumber=${pageNumber}&ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalogID}`;

    this.httpClient.get<ZadaciPaged>(urlZad2).subscribe((x: ZadaciPaged) => {
      this.zad = x.zadaci.dataItems;
      this.trenutnaStranica = x.zadaci.currentPage;
      this.ukupnoStranica = x.zadaci.totalPages;

      this.provjeriDaLiJePrazna();
    });
  }
  idiNaStranicu(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.ukupnoStranica) {
      this.loadZadaciPaged(pageNumber);
    }
  }
  nizStranica() {
    return Array.from({length: this.ukupnoStranica}, (_, i) => i + 1);
  }
  provjeriDaLiJePrazna() {
    if (this. daLiJeTrenutnaPrazna() && this.trenutnaStranica > 1) {
      this.goToPage(this.trenutnaStranica - 1);
    }
  }
  daLiJeTrenutnaPrazna(): boolean {
    return this.daLiJePrazna(this.zad);
  }
  daLiJePrazna(art: DataItemZadaci[]): boolean {
    return art.length === 0;
  }


  loadArtikliPaged(pageNumber: number) {

    const artPoStranici = 3;
    const urlArt2 = MojConfig.server_adresa+`/Artikal-GetAllKojiNisuObrisaniPaged?pageNumber=${pageNumber}&pageSize=${artPoStranici}`;

    this.httpClient.get<Artikli>(urlArt2).subscribe((x: Artikli) => {
      this.art = x.artikli.dataItems;
      this.currentPage = x.artikli.currentPage;
      this.totalPages = x.artikli.totalPages;

      this.provjeriJelPrazna();
    });
  }

  narudzbe:DataItemNarudzbe[]=[];
  currentPageNarudzbe: number = 1;
  totalPagesNarudzbe: number = 1;
  loadNarudzbe(pageNumber: number) {

    const narPoStr = 3;
    const url = MojConfig.server_adresa+`/Narudzba-GetAllPaged?PageSize=${narPoStr}&PageNumber=${pageNumber}`;

    this.httpClient.get<Narudzbe>(url).subscribe((x) => {
      this.narudzbe = x.narudzbe.dataItems;
      this.currentPageNarudzbe = x.narudzbe.currentPage;
      this.totalPagesNarudzbe = x.narudzbe.totalPages;

      this.provjeriJelPraznaNarudzba();
    });
  }
  goToPage(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.totalPages) {
      this.loadArtikliPaged(pageNumber);
    }
  }

  getPagesArray() {
    return Array.from({length: this.totalPages}, (_, i) => i + 1);
  }

  provjeriJelPrazna() {
    if (this.jelTrenutnaPrazna() && this.currentPage > 1) {
      this.goToPage(this.currentPage - 1);
    }
  }

  jelTrenutnaPrazna(): boolean {
    return this.jelPrazna(this.art);
  }

  jelPrazna(art: DataItem[]): boolean {
    return art.length === 0;
  }
  refreshTimestamp: number = Date.now();

  otvoriDodavanje() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "50%";
    const dialogRef=this.dialog.open(DodavanjeArtiklaComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((data:any)=>{
      this.loadArtikliPaged(this.currentPage);
    })
  }

  otvoriDijalog(id: number) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "100%";
    // @ts-ignore
    const dialogRef=this.dialog.open(UpdateArtiklaComponent, {data: {vrijednost: id}}, dialogConfig);

    dialogRef.afterClosed().subscribe((data: any) => {
      this.loadArtikliPaged(this.currentPage);
      this.refreshTimestamp = Date.now();
    });
  }

  protected readonly MojConfig = MojConfig;
  protected readonly Array = Array;


  prikaziArtikliZadaci: PrikaziArtikliZadaci = {neobrisani: true, obrisani: false, mojiZadaci: false, aktivneNarudzbe:false, dokumenti:false}

  prikaziRadSaArtiklima(event: any) {
    this.prikaziArtikliZadaci.neobrisani = true;
    this.prikaziArtikliZadaci.obrisani = false;
    this.prikaziArtikliZadaci.mojiZadaci = false;
    this.prikaziArtikliZadaci.aktivneNarudzbe=false;
    this.prikaziArtikliZadaci.dokumenti=false;
    this.Odaberi(event);
  }

  prikaziObrisane(event: any) {
    this.prikaziArtikliZadaci.neobrisani = false;
    this.prikaziArtikliZadaci.obrisani = true;
    this.prikaziArtikliZadaci.mojiZadaci = false;
    this.prikaziArtikliZadaci.aktivneNarudzbe=false;
    this.prikaziArtikliZadaci.dokumenti=false;
    this.Odaberi(event);
  }

  prikaziMojeZadatke(event: any) {
    this.prikaziArtikliZadaci.neobrisani = false;
    this.prikaziArtikliZadaci.obrisani = false;
    this.prikaziArtikliZadaci.mojiZadaci = true;
    this.prikaziArtikliZadaci.aktivneNarudzbe=false;
    this.prikaziArtikliZadaci.dokumenti=false;
    this.Odaberi(event);
  }

  prikaziAktivneNarudzbe(event: any) {
    this.prikaziArtikliZadaci.neobrisani = false;
    this.prikaziArtikliZadaci.obrisani = false;
    this.prikaziArtikliZadaci.mojiZadaci = false;
    this.prikaziArtikliZadaci.aktivneNarudzbe=true;
    this.prikaziArtikliZadaci.dokumenti=false;
    this.Odaberi(event);
  }

  prikaziDokumenti(event:any){
    this.prikaziArtikliZadaci.neobrisani = false;
    this.prikaziArtikliZadaci.obrisani = false;
    this.prikaziArtikliZadaci.mojiZadaci = false;
    this.prikaziArtikliZadaci.aktivneNarudzbe=false;
    this.prikaziArtikliZadaci.dokumenti=true;
    this.Odaberi(event);
  }
  Odaberi(event: any) {
    let buttons = document.querySelectorAll('.navbarButton');
    buttons.forEach(element => {
      element.classList.remove('selected');
      element.classList.add('not_selected');
    });
    event.target.classList.add('selected');
  }


  obrisani: DataItemObrisani[] = [];
  currentPageObrisani: number = 1;
  totalPagesObrisani: number = 1;
  private loadObrisani(pageNumber: number) {
    const artPoStranici = 3;
    const urlArt2 = MojConfig.server_adresa + `/Artikal-GetAllKojiSuObrisaniPaged?pageNumber=${pageNumber}&pageSize=${artPoStranici}`;

    this.httpClient.get<ArtikliResponse>(urlArt2).subscribe((x: ArtikliResponse) => {
      this.obrisani = x.artikli.dataItems;
      this.currentPageObrisani = x.artikli.currentPage;
      this.totalPagesObrisani = x.artikli.totalPages;

      this.provjeriJelPraznaObrisani();

    });
  }

  vrati(artikal: number) {
    const obrItem={
      artID:artikal
    }
    this.dialogService.openConfirmDialog('Da li ste sigurni da želite vratiti obrisani artikal u katalog artikal?')
      .afterClosed().subscribe(res=>{
      if(res==true){

        let urlObr=MojConfig.server_adresa+`/Artikal-DodajUNeobrisane`
        this.httpClient.post(urlObr, obrItem).subscribe((res:any)=>{
          this.ngOnInit();
        })
        this.dialogService.openOkDialog("Artikal uspješno vraćen u katalog!");


      }

    });
  }

  goToPageObrisani(number: number) {
    if (number >= 1 && number <= this.totalPagesObrisani) {
      this.loadObrisani(number);
    }
  }

  provjeriJelPraznaObrisani() {
    if (this.jelTrenutnaPraznaObrisani() && this.currentPageObrisani > 1) {
      this.goToPage(this.currentPageObrisani - 1);
    }
  }

  jelTrenutnaPraznaObrisani() {
    return this.jelPraznaObrisani(this.obrisani);

  }

  jelPraznaObrisani(obrisani: DataItemObrisani[]) {
    return obrisani.length===0;
  }

  getPagesArrayObrisani() {
    return Array.from({length: this.totalPagesObrisani}, (_, i) => i + 1);
  }


  zadaci: StavkeZadatakResponseStavke[] = [];

  Obrisi(id: number) {
    const ZadUrl=MojConfig.server_adresa+`/Zadaci-Obrisi?ID=${id}`;
    this.httpClient.delete(ZadUrl).subscribe((res:any):void=>{
      this.dialogService.openOkDialog("Zadatak uspjesno odradjen!");
      this.ngOnInit();
    })
  }

  private fetchZadatke() {
    if (this.myAuth.isAdmin()) {
      const zadaciUrl = MojConfig.server_adresa + `/Zadaci-GetAll`;
      this.httpClient.get<StavkeZadatakResponse>(zadaciUrl).subscribe((data) => {
        this.zadaci = data.stavkeZadatak;
      });
    } else if (this.myAuth.isRadnik()) {
      const zadatakUrl = MojConfig.server_adresa + `/Zadaci-GetByID?ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalogID}`;
      this.httpClient.get<ZadaciGetByIdResponse>(zadatakUrl).subscribe((data) => {
        this.zadaci = data.zadaci;
      });
    }
  }

  goToPageNarudzbe(number: number) {
    if (number >= 1 && number <= this.totalPagesNarudzbe) {
      this.loadNarudzbe(number);
    }
  }

  getPagesArrayNarudzbe() {
    return Array.from({length: this.totalPagesNarudzbe}, (_, i) => i + 1);

  }

  provjeriJelPraznaNarudzba() {
    if (this.jelTrenutnaPraznaNarudzba() && this.currentPageNarudzbe > 1) {
      this.goToPageNarudzbe(this.currentPageNarudzbe - 1);
    }
  }

  private jelTrenutnaPraznaNarudzba() {
    return this.jelPraznaNarudzbe(this.narudzbe);

  }

  private jelPraznaNarudzbe(narudzbe: DataItemNarudzbe[]) {
    return narudzbe.length===0;

  }

  dokumenti:DokumentiPagedResponse[]=[];
  totalPagesDokumenti: number = 1;
  currentPageDokumenti:number=1;
  dohvatiDokumente(currentPage:number){
    const dokPoStr = 3;

    let url=MojConfig.server_adresa+`/DokumentGet?PageNumber=${currentPage}&PageSize=${dokPoStr}`;
    this.httpClient.get<DokumentiPaged>(url).subscribe((x=>{
      this.dokumenti=x.dataItems;
      this.totalPagesDokumenti=x.totalPages;
      this.currentPageDokumenti=x.currentPage;
      this.provjeriJelPrazniDokumenti();
    }));
  }

  getPagesArrayDokumenti() {
    return Array.from({length: this.totalPagesDokumenti}, (_, i) => i + 1);

  }

  goToPageDokumenti(number: number) {
    if (number >= 1 && number <= this.totalPagesDokumenti) {
      this.dohvatiDokumente(number);
    }
  }

  provjeriJelPrazniDokumenti() {
    if (this.jelTrenutnaPraznaDokumenti() && this.currentPageDokumenti > 1) {
      this.goToPageDokumenti(this.currentPageDokumenti - 1);
    }
  }

  private jelTrenutnaPraznaDokumenti() {
    return this.jelPraznaDokumenti(this.dokumenti);

  }

  private jelPraznaDokumenti(dokumenti: DokumentiPagedResponse[]) {
    return dokumenti.length===0;

  }
}
