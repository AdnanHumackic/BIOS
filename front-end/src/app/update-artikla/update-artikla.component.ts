import {Component, EventEmitter, Inject, OnInit, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  Validators
} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {SharedServiceService} from "../shared-services/shared-service.service";
import {ArtikalGetByID} from "../pregled-detalja/artikal-get-by-id";
import {ArtikalUpdateRequest} from "./update-artikla-request";
import {MojConfig} from "../moj-config";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogConfig,
  MatDialogContent, MatDialogRef
} from "@angular/material/dialog";
import {UpdateKompatibilnihComponent} from "../update-kompatibilnih/update-kompatibilnih.component";
import {ArtikalPretragaResponseArtikal} from "../pregled-artikala/artikli-pretraga-response";
import {KompatibilnostGetByIdResponseKompatibilnost, KompGetByIDResponse} from "./kompatibilnost-get-by-id";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {TipGetAllResponse, TipGetAllResponseTip} from "../dodavanje-artikla/tip-get-all";
import {ArtikliGetAllResponse, ArtikliGetAllResponseArtikli} from "../dodavanje-kompatibilnih/artikal-get-all";
import {DomSanitizer, SafeUrl} from "@angular/platform-browser";
import {Root, SlikeResponse} from "../pregled-detalja/get-slike";
import {PrikaziAdmin} from "../dodavanje-radnika/prikazi-admin";
import {PrikaziUpdate} from "./prikaziUpdate";
import {
  KompatibilnostGetAllResponse,
  KompatibilnostGetAllResponseKompatibilnost
} from "../dodavanje-kompatibilnih/kompatibilnost-get-all";
import {
  ArtikalGetAllOsimProslijedjenogResponse,
  ArtikalGetAllOsimProslijedjenogResponseArtikal
} from "../update-kompatibilnih/get-all-osim-proslijedjenog";
import {UpravljanjeArtiklimaComponent} from "../upravljanje-artiklima/upravljanje-artiklima.component";

@Component({
  selector: 'app-update-artikla',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatIconModule, MatDialogContent, MatDialogClose, HttpClientModule, ReactiveFormsModule, MatDialogActions],
  templateUrl: './update-artikla.component.html',
  styleUrl: './update-artikla.component.css'
})
export class UpdateArtiklaComponent implements OnInit {
  constructor(private route: ActivatedRoute, private httpClient: HttpClient,
              private dialogService: DialogServiceService,
              @Inject(MAT_DIALOG_DATA) public data: any, private sanitizer: DomSanitizer) {
  }
  @Output() artikalAzuriran: EventEmitter<any> = new EventEmitter();

  art: ArtikalPretragaResponseArtikal = {
    id: Number(this.route.snapshot.paramMap.get("id")),
    naziv: "", cijena: 0, opis: "", proizvodjac: "", tip: ""
  };
  public artikalGetByID!: ArtikalGetByID;


  public kompatibilnostGetById: KompatibilnostGetByIdResponseKompatibilnost[] = [];


  tip: TipGetAllResponseTip[] = [];


  ngOnInit(): void {
    this.UcitajPodatkeArtikla(this.data.vrijednost);
    let url = MojConfig.server_adresa + `/TipArtikla-GetAll`;
    this.httpClient.get<TipGetAllResponse>(url).subscribe((x: TipGetAllResponse) => {
      this.tip = x.tip;
    })
    this.getAll();
    this.ucitajSveOsimProslijednjog();
    this.dohvati(this.data.vrijednost);
    this.getSveKompatibilnost();
    this.ucitajKompatibilne(this.data.vrijednost);
  }

  dohvatanjeArt: ArtikliGetAllResponseArtikli[] = [];

  getAll() {

    let urlArtikala = MojConfig.server_adresa + `/Artikal-GetAll`;
    this.httpClient.get<ArtikliGetAllResponse>(urlArtikala).subscribe((x: ArtikliGetAllResponse) => {
      this.dohvatanjeArt = x.artikli;
      console.log(this.dohvatanjeArt);
    })
  }

  urediArtikal = new FormGroup({
    id: new FormControl(''),
    imeArtikla: new FormControl(''),
    cijena: new FormControl(''),
    proizvodjac: new FormControl(''),
    opis: new FormControl(''),
    tipID: new FormControl('')
  });

  updateArtikal() {
    const formData = {
      id: this.artikalGetByID.id,
      imeArtikla: this.urediArtikal.get('imeArtikla')?.value,
      cijena: this.urediArtikal.get('cijena')?.value,
      proizvodjac: this.urediArtikal.get('proizvodjac')?.value,
      opis: this.urediArtikal.get('opis')?.value,
      tipID: this.urediArtikal.get('tipID')?.value
    };
    if (this.urediArtikal.valid) {
      let url = MojConfig.server_adresa + `/Artikal-Update`;
      this.httpClient.post(url, formData).subscribe((x) => {
      })
      // @ts-ignore
      this.prikazKompatibilni();
    }
    //test
    const azuriraniArtikal={
      id: this.artikalGetByID.id,
      imeArtikla: this.urediArtikal.get('imeArtikla')?.value,
      cijena: this.urediArtikal.get('cijena')?.value,
      proizvodjac: this.urediArtikal.get('proizvodjac')?.value,
      opis: this.urediArtikal.get('opis')?.value,
      tipID: this.urediArtikal.get('tipID')?.value
    }
    this.artikalAzuriran.emit(azuriraniArtikal);
  }

  ObrisiKompatibilniElement($event: MouseEvent, kompatibilnostID: number) {
    this.dialogService.openConfirmDialog('Da li ste sigurni da želite orbisati kompatibilni artikal?')
      .afterClosed().subscribe(res => {
      if (res == true) {

        let url = MojConfig.server_adresa + `/Kompatibilnost-Obrisi?ID=${kompatibilnostID}`
        this.httpClient.delete(url).subscribe((res: any) => {
          this.ngOnInit();
        })
      }
    });
  }

  ucitajKompatibilne(id: any) {
    let urlKomp = MojConfig.server_adresa + `/Kompatibilnost-GetByID?ID=${id}`;
    this.httpClient.get<KompGetByIDResponse>(urlKomp).subscribe((y: KompGetByIDResponse) => {
      this.kompatibilnostGetById = y.komp;
    })
  }

  UcitajPodatkeArtikla(id: number) {
    let url = MojConfig.server_adresa + `/Artikal-GetByID?ID=${id}`;
    this.httpClient.get<ArtikalGetByID>(url).subscribe((x: ArtikalGetByID) => {
      this.artikalGetByID = x;
      // @ts-ignore
      this.urediArtikal = new FormGroup({
        id: new FormControl(x['id']),
        imeArtikla: new FormControl(x['naziv'], [Validators.required, this.imeValidator.bind(this)]),
        cijena: new FormControl(x['cijena'], [Validators.required, this.cijenaValidator.bind(this)]),
        slika: new FormControl(x['slika']),
        proizvodjac: new FormControl(x['proizvodjac'], [Validators.required, Validators.pattern(/^[A-Z][a-zA-Z]*$/)]),
        opis: new FormControl(x['opis'], Validators.required),
        tipID: new FormControl(x['tipID'], Validators.required),
      });
    })
  }

  imeValidator(control: AbstractControl): ValidationErrors | null {
    const ime = control.value;

    var postoji: boolean = false;
    let sviArt = this.dohvatanjeArt;
    sviArt.forEach((x) => {
      if (x.naziv == ime) {
        postoji = true;
      }
    })


    if (postoji) {
      return {artikalPostoji: true};
    } else {
      return null;
    }

  }

  cijenaValidator(control: AbstractControl): ValidationErrors | null {
    const cijena = parseFloat(control.value);

    if (isNaN(cijena) || cijena < 0) {
      return {'cijenaInvalid': true};
    }

    return null;
  }

  protected readonly MojConfig = MojConfig;


  slike: SafeUrl[] = [];
  slikeSaRb: { redniBroj: number, slika: string }[] = [];

  dohvati(id: number) {

    let url = MojConfig.server_adresa + `/ArtikalSlika/ArtikalSlika?id=${id}`;
    this.httpClient.get<{ redniBroj: number, slika: string }[]>(url).subscribe(data => {
      this.slikeSaRb = data;
      this.slike = data.map(item => this.sanitizer.bypassSecurityTrustUrl(`data:image/jpeg;base64,${item.slika}`));
    });
  }

  currentIndex: number = 0;

  nextSlide() {
    this.currentIndex = (this.currentIndex + 1) % this.slike.length;

  }

  prevSlide() {
    this.currentIndex = (this.currentIndex - 1 + this.slike.length) % this.slike.length;

  }


  prikaz: PrikaziUpdate = {editovanjeOsnovnih: true, editovanjeKompatibilnih: false, editovanjeSlika: false};


  prikaziOsnovne() {
    this.prikaz.editovanjeOsnovnih = true;
    this.prikaz.editovanjeKompatibilnih = false;
    this.prikaz.editovanjeSlika = false;
    this.odaberi();
  }

  prikazKompatibilni() {
    this.prikaz.editovanjeOsnovnih = false;
    this.prikaz.editovanjeKompatibilnih = true;
    this.prikaz.editovanjeSlika = false;
    this.odaberi();
  }

  prikazSlike() {
    this.prikaz.editovanjeOsnovnih = false;
    this.prikaz.editovanjeKompatibilnih = false;
    this.prikaz.editovanjeSlika = true;
    this.odaberi();
  }

  odaberi() {
    let buttons = document.querySelectorAll('.navbarButton');
    buttons.forEach(element => {
      element.classList.remove('selected');
      element.classList.add('not_selected');
    });
    if (this.prikaz.editovanjeOsnovnih) {
      document.getElementById('osnovneButton')?.classList.add('selected');
    } else if (this.prikaz.editovanjeKompatibilnih) {
      document.getElementById('kompatibilniButton')?.classList.add('selected');
    } else if (this.prikaz.editovanjeSlika) {
      document.getElementById('slikeButton')?.classList.add('selected');
    }
  }

  kompatibilnostForma = new FormGroup({
    artikal1: new FormControl(''),
    artikal2: new FormControl('',[Validators.required, this.provjeriPostojanje.bind(this)] )

  });
  kompatibilnost: KompatibilnostGetAllResponseKompatibilnost[] = [];

  getSveKompatibilnost() {
    let url = MojConfig.server_adresa + `/Kompatibilnost-GetAll`;
    this.httpClient.get<KompatibilnostGetAllResponse>(url).subscribe((x: KompatibilnostGetAllResponse) => {
      this.kompatibilnost = x.kompatibilnost;
    })
  }

  provjeriPostojanje(control: AbstractControl): ValidationErrors | null {
    if (!this.kompatibilnost) {
      return null;
    }

    const artikal1ID = this.data.vrijednost;
    const artikal2ID = control.value;

    var postoji: boolean = false;
    let sveKomp = this.kompatibilnost;
    sveKomp.forEach((x) => {
      if (x.art1ID == artikal1ID && x.art2ID == artikal2ID) {
        postoji = true;
      }
    })


    if (postoji) {
      return {kompatibilnostPostoji: true};
    } else {
      return null;
    }
  }


  kreirajKompatibilnost() {

    const formData = {
      artikal1: this.data.vrijednost,
      artikal2: this.kompatibilnostForma.get('artikal2')?.value
    }
    if (this.kompatibilnostForma.valid) {

      let url = MojConfig.server_adresa + `/Kompatibilnost-Dodaj`;
      this.httpClient.post(url, formData).subscribe((res) => {
      })
      this.kompatibilnostForma.reset();
    }
  }

  artikli: ArtikalGetAllOsimProslijedjenogResponseArtikal[] = [];

  ucitajSveOsimProslijednjog() {
    let url = MojConfig.server_adresa + `/Artikal-GetAllOsimProslijednjenog?ID=${this.data.vrijednost}`;

    this.httpClient.get<ArtikalGetAllOsimProslijedjenogResponse>(url).subscribe((x: ArtikalGetAllOsimProslijedjenogResponse) => {
      this.artikli = x.artikal;
    })
  }


  zavrsi() {
    this.prikazSlike();
  }

  prazanDiv: boolean = false;

  obrisiSliku(i: number) {
    const artikalId = this.artikalGetByID.id;
    const rb = this.slikeSaRb[i].redniBroj;
    let url = MojConfig.server_adresa + `/ArtikalSlika-Obrisi?artikalId=${artikalId}&redniBrojSlike=${rb}`;

    this.httpClient.delete(url).subscribe((x => {
      this.dohvati(artikalId);
      if (this.slike.length === 0) {
        this.prazanDiv = true;
      } else {
        this.prazanDiv = false;
      }
    }))

  }


  dodavanjeSlike() {
    if (this.slikePreview.length === 0) {
      this.dialogService.openOkDialog("Molimo vas da odaberete slike za artikal!")
    } else {
      const request = {
        IDArtikla: this.artikalGetByID.id,
        SlikaArtikla: this.slikePreview
      };

      let url = MojConfig.server_adresa + `/ArtikalSlika-Update`;
      this.httpClient.post(url, request).subscribe((x => {
        this.slikePreview = [];
      }));
      const slike={
        IDArtikla: this.artikalGetByID.id,
        SlikaArtikla: this.slikePreview
      }
      this.artikalAzuriran.emit(slike);
    }

  }

  slikePreview: string[] = [];

  generisi_preview(event: any) {
    const files = event.target.files;
    if (files && files.length > 0) {
      for (let i = 0; i < files.length; i++) {
        const reader = new FileReader();

        reader.onload = (e: any) => {
          this.slikePreview.push(e.target.result);
        };

        reader.readAsDataURL(files[i]);
      }
    }
  }

}
