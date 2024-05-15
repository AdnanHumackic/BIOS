import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ArtikalGetByID} from "./artikal-get-by-id";
import {MojConfig} from "../moj-config";
import {SharedServiceService} from "../shared-services/shared-service.service";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router, RouterLink, RouterLinkActive} from "@angular/router";
import {ArtikalPretragaResponseArtikal} from "../pregled-artikala/artikli-pretraga-response";
import {KompatibilnostGetByIdResponseKompatibilnost, KompGetByIDResponse} from "./kompatibilnost-get-by-id";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {KompatibilnostDetaljiComponent} from "../kompatibilnost-detalji/kompatibilnost-detalji.component";
import {SlikeResponse} from "./get-slike";
import {DomSanitizer, SafeUrl} from "@angular/platform-browser";
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {RecenzijaGetbyIdResponse} from "./recenzije-get-by-id";
import {MyAuthService} from "../shared-services/MyAuthService";
import {IdValidanService} from "../helper/auth/id-validan-service";
import {DataItemRecenzije, RecenzijePaged} from "./recenzije-get-by-id-paged";

export interface Recenzija {
  artikalId: number;
  sadrzaj: string;
  datumDodavanja: string;
  korisnikId: number | undefined;
  korisnickoIme: string
}

@Component({
  selector: 'app-pregled-detalja',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, FormsModule, ReactiveFormsModule],
  templateUrl: './pregled-detalja.component.html',
  styleUrl: './pregled-detalja.component.css',
})

export class PregledDetaljaComponent implements OnInit {
  recenzije: Recenzija[] = [];
  prikazi: boolean = false;
  snimanjeRecenzije: boolean = false;

  constructor(private route: ActivatedRoute, public httpClient: HttpClient,
              public dialogService: DialogServiceService, private dialog: MatDialog, private router: Router, private sanitizer: DomSanitizer,
              private fb: FormBuilder, public MyAuth: MyAuthService, private idValidan:IdValidanService) {
  }

  public artikalGetByID!: ArtikalGetByID;
  art: ArtikalPretragaResponseArtikal = {
    id: Number(this.route.snapshot.paramMap.get("id")),
    naziv: "", cijena: 0, opis: "", proizvodjac: "", tip: ""
  };
  kompatibilnostGetById: KompatibilnostGetByIdResponseKompatibilnost[] = [];

  recenzijaRequest: Recenzija = {
    artikalId: 0,
    sadrzaj: '',
    datumDodavanja: '',
    korisnikId: 0,
    korisnickoIme: ''
  };
  rec: DataItemRecenzije[]=[];
  trenutnaStranica:number=1;
  ukupnoStranica:number=1;
  decrypt(encodedId: string): number {
    const decodedId = atob(encodedId);
    return +decodedId;
  }
  ngOnInit(): void {
    // @ts-ignore
    let id = this.decrypt(this.route.snapshot.paramMap.get('id'));


    let url = MojConfig.server_adresa + `/Artikal-GetByID?ID=${id}`;
    this.httpClient.get<ArtikalGetByID>(url).subscribe((x: ArtikalGetByID) => {
      this.artikalGetByID = x;
      this.recenzijaRequest.artikalId = this.artikalGetByID.id;
      this.ucitajKompatibilne(id);
      this.dohvati(id);
      this.loadRecenzijePaged(this.trenutnaStranica);
    })

    this.ucitajKompatibilne(id);
    this.dohvati(id);


    this.route.params.subscribe(x=>{
      this.idValidan.provjeriId(id).subscribe(valid=>{
        if(!valid){
          this.router.navigate(['/error404']);
        }
      })
    })
  }

  recenzijaForm = new FormGroup({
    sadrzaj: new FormControl('', Validators.required),
  });


  private loadRecenzijePaged(pageNumber: number) {
    const zadPoStranici = 3;
    const urlRec2 = MojConfig.server_adresa+`/Recenzija-GetByIDPaged?PageSize=${zadPoStranici}&PageNumber=${pageNumber}&ID=${this.artikalGetByID.id}`;

    this.httpClient.get<RecenzijePaged>(urlRec2).subscribe((x:RecenzijePaged)=>{
      this.rec=x.recenzije.dataItems;
      this.trenutnaStranica=x.recenzije.currentPage;
      this.ukupnoStranica=x.recenzije.totalPages;

      this.provjeriDaLiJePrazna();
    });
  }
   provjeriDaLiJePrazna() {
    if(this.daLiJeTrenutnaPrazna() && this.trenutnaStranica > 1){
      this.goToPage(this.trenutnaStranica-1);
    }
  }
  goToPage(pageNumber:number){
    if(pageNumber>=1 && pageNumber <= this.ukupnoStranica){
      this.loadRecenzijePaged(pageNumber);
    }
  }
  nizStranica(){
    return Array.from({length:this.ukupnoStranica}, (_, i) => i + 1);
  }
  daLiJeTrenutnaPrazna(){
    return this.daLiJePrazna(this.rec);
  }
  daLiJePrazna(art:DataItemRecenzije[]){
    return art.length === 0;
  }
  snimiRecenziju() {
    if (this.recenzijaForm.invalid) {
      this.recenzijaForm.markAllAsTouched();
      return;
    }

    if (this.snimanjeRecenzije) return;

    this.snimanjeRecenzije = true;

    const formData = {
      sadrzaj: this.recenzijaForm.get('sadrzaj')?.value,
      artikalId: this.artikalGetByID.id,
      evidentiraoKorisnikId: this.MyAuth.getAuthorizationToken()?.korisnickiNalog.id,
    }
    const recenzijaUrl = MojConfig.server_adresa + `/Recenzija-Dodaj`;
    this.httpClient.post(recenzijaUrl, formData).subscribe(
      (response: any) => {
        this.dialogService.openOkDialog('Recenzija uspješno snimljena!');
        this.snimanjeRecenzije = false;
        this.recenzijaForm.reset();
      },
      (error) => {
        this.dialogService.openOkDialog(
          'Greška prilikom snimanja recenzije!'
        );
        this.snimanjeRecenzije = false;
      }
    );
  }

  prikaziRecenzije() {
    this.prikazi = !this.prikazi;
    if (this.prikazi) {
      const recenzijeUrl = MojConfig.server_adresa + `/Recenzije-GetById?ID=${this.artikalGetByID.id}`;
      this.httpClient.get<RecenzijaGetbyIdResponse>(recenzijeUrl)
        .subscribe(response => {
          // @ts-ignore
          this.recenzije = response.recenzija;
        });
    }
    else if(this.recenzije.length===0){
      this.dialogService.openOkDialog("Za odabrani artikal nema recenzija!")
    }
  }

  ucitajKompatibilne(id: any) {
    let urlKomp = MojConfig.server_adresa + `/Kompatibilnost-GetByID?ID=${id}`;
    this.httpClient.get<KompGetByIDResponse>(urlKomp).subscribe((y: KompGetByIDResponse) => {
      this.kompatibilnostGetById = y.komp;
    })
  }

  provjeri() {
    let sve = this.kompatibilnostGetById;
    var postoji: boolean = false;
    sve.forEach((x) => {
      if (x.kompatibilnostID != 0 && x.artikal2Ime != "" && x.id != 0) {
        postoji = true;
      }
    })
    if (postoji == false) {
      this.dialogService.openOkDialog("Za artikal nema dodanih kompatibilnih elemenata!")
    }
  }


  otvoriDetalje(id: number) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    // @ts-ignore
    this.dialog.open(KompatibilnostDetaljiComponent, {data: {vrijednost: id}}, dialogConfig);
  }

  slike: SafeUrl[] = [];
  protected readonly MojConfig = MojConfig;

  dohvati(id: number) {
    let url = MojConfig.server_adresa + `/ArtikalSlika/ArtikalSlika?id=${id}`;
    this.httpClient.get<SlikeResponse[]>(url).subscribe(data => {
      this.slike = data.map(slika => this.sanitizer.bypassSecurityTrustUrl(`data:image/jpeg;base64,${slika.slika}`));
    });

  }

  currentIndex: number = 0;

  nextSlide() {
    this.currentIndex = (this.currentIndex + 1) % this.slike.length;

  }

  prevSlide() {
    this.currentIndex = (this.currentIndex - 1 + this.slike.length) % this.slike.length;

  }

}
