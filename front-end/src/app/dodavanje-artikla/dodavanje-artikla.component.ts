import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {
  AbstractControl, AsyncValidatorFn,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors, ValidatorFn,
  Validators
} from "@angular/forms";
import {TipGetAllResponse, TipGetAllResponseTip} from "./tip-get-all";
import {MojConfig} from "../moj-config";
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent, MatDialogRef,
} from "@angular/material/dialog";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {MatButtonModule} from "@angular/material/button";
import {ZadnjeKreirani} from "../dodavanje-kompatibilnih/get-zadnje-kreirani-artikal";
import {MatIconModule} from "@angular/material/icon";
import {ArtikliGetAllResponse, ArtikliGetAllResponseArtikli} from "../dodavanje-kompatibilnih/artikal-get-all";
import {PrikaziZaArtikle} from "./dodavanje-art";
import {
  KompatibilnostGetAllResponse,
  KompatibilnostGetAllResponseKompatibilnost
} from "../dodavanje-kompatibilnih/kompatibilnost-get-all";

@Component({
  selector: 'app-dodavanje-artikla',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatIconModule, HttpClientModule, MatDialogClose, MatDialogContent, ReactiveFormsModule, MatDialogActions],
  templateUrl: './dodavanje-artikla.component.html',
  styleUrl: './dodavanje-artikla.component.css'
})
export class DodavanjeArtiklaComponent implements OnInit {

  @Output() artikalDodan: EventEmitter<any> = new EventEmitter();
  ngOnInit(): void {
    let url = MojConfig.server_adresa + `/TipArtikla-GetAll`;
    this.httpClient.get<TipGetAllResponse>(url).subscribe((x: TipGetAllResponse) => {
      this.tip = x.tip;
    })

    this.getAll();
    //this.getAllArtikli(this.httpClient);
    this.getSveKompatibilnost();
  }

  dohvatanjeArt: ArtikliGetAllResponseArtikli[] = [];

  getAll() {

    let urlArtikala = MojConfig.server_adresa + `/Artikal-GetAll`;
    this.httpClient.get<ArtikliGetAllResponse>(urlArtikala).subscribe((x: ArtikliGetAllResponse) => {
      this.dohvatanjeArt = x.artikli;
      console.log(this.dohvatanjeArt);
    })
  }

  constructor(private httpClient: HttpClient, private dialogService: DialogServiceService,
              public dialogRef:MatDialogRef<DodavanjeArtiklaComponent>) {
  }

  tip: TipGetAllResponseTip[] = [];

  zadnjiID!: number;
  zadnjeKreirani!: ZadnjeKreirani;

  getZadnjeKreirani() {
    let url = MojConfig.server_adresa + `/Artikal-GetZadnjeKreirani`
    this.httpClient.get<ZadnjeKreirani>(url).subscribe((x: ZadnjeKreirani) => {
      this.zadnjeKreirani = x;
      this.zadnjiID = this.zadnjeKreirani.id;
      console.log(this.zadnjiID);
      console.log(this.zadnjeKreirani)
    })
  }

  vrijednost!: any;


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

  kompatibilnost: KompatibilnostGetAllResponseKompatibilnost[] = [];

  getSveKompatibilnost() {
    let url = MojConfig.server_adresa + `/Kompatibilnost-GetAll`;
    this.httpClient.get<KompatibilnostGetAllResponse>(url).subscribe((x: KompatibilnostGetAllResponse) => {
      this.kompatibilnost = x.kompatibilnost;
      console.log(this.kompatibilnost);
    })
  }

  dodavanjeSlike() {
    if (this.slikePreview.length === 0) {
      this.dialogService.openOkDialog("Molimo vas da odaberete slike za artikal!")
    } else {
      const request = {
        IDArtikla: this.zadnjiID,
        SlikaArtikla: this.slikePreview
      };

      let url = MojConfig.server_adresa + `/ArtikalSlika-Update`;
      this.httpClient.post(url, request).subscribe((x => {
        this.slikePreview = [];
      }));
      this.prikaziDodavanjeKompatibilnih()
    }

  }


  dodavanjeArtikla = new FormGroup({
    imeArtikla: new FormControl('', /*{*/
      /*validators:*/ [Validators.required,this.imeValidator.bind(this)]
      /*asyncValidators: ],
      updateOn: 'blur'
    }*/),
    cijena: new FormControl('', [Validators.pattern(/^\d+$/), this.cijenaValidator.bind(this)]),
    tipID: new FormControl('', Validators.required),
    proizvodjac: new FormControl('', [Validators.required, Validators.pattern(/^[A-Z][a-zA-Z]*$/)]),
    opis: new FormControl('', Validators.required),
  });

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


  dodajArtikal() {

    if (this.dodavanjeArtikla.valid) {

      const formData = {
        imeArtikla: this.dodavanjeArtikla.get('imeArtikla')?.value,
        proizvodjac: this.dodavanjeArtikla.get('proizvodjac')?.value,
        cijena: this.dodavanjeArtikla.get('cijena')?.value,
        tipID: this.dodavanjeArtikla.get('tipID')?.value,
        opis: this.dodavanjeArtikla.get('opis')?.value,
      };
      console.log(formData);
      let url = MojConfig.server_adresa + `/Artikal-Dodaj`;
      this.httpClient.post(url, formData).subscribe((x => {
        console.log('ok');

      }))
      this.dialogService.openOkDialog("Artikal uspješno dodat, molimo vas da dodate slike za artikal").afterClosed().subscribe((x => {
        if (x == true) {
          this.dodavanjeArtikla.reset();
          this.prikaziDodavanjeSlika();
        }
      }));
    }

    const noviArtikal={
      imeArtikla: this.dodavanjeArtikla.get('imeArtikla')?.value,
      proizvodjac: this.dodavanjeArtikla.get('proizvodjac')?.value,
      cijena: this.dodavanjeArtikla.get('cijena')?.value,
      tipID: this.dodavanjeArtikla.get('tipID')?.value,
      opis: this.dodavanjeArtikla.get('opis')?.value,
    }
    this.artikalDodan.emit(noviArtikal);
  }

  prikaz: PrikaziZaArtikle = {
    prikaziDodavanjeDetalja: true,
    prikaziDodavanjeSlika: false,
    prikaziDodavanjeKompatibilnih: false
  };

  prikaziDodavanjeDetalja() {
    this.prikaz.prikaziDodavanjeDetalja = true;
    this.prikaz.prikaziDodavanjeSlika = false;
    this.prikaz.prikaziDodavanjeKompatibilnih = false;
    this.odaberi()

  }

  prikaziDodavanjeSlika() {
    this.prikaz.prikaziDodavanjeDetalja = false;
    this.prikaz.prikaziDodavanjeSlika = true;
    this.prikaz.prikaziDodavanjeKompatibilnih = false;
    this.odaberi();
  }

  prikaziDodavanjeKompatibilnih() {
    this.prikaz.prikaziDodavanjeDetalja = false;
    this.prikaz.prikaziDodavanjeSlika = false;
    this.prikaz.prikaziDodavanjeKompatibilnih = true;
    this.odaberi();
  }

  odaberi() {
    let buttons = document.querySelectorAll('.navbarButton');
    buttons.forEach(element => {
      element.classList.remove('selected');
      element.classList.add('not_selected');
    });
    if (this.prikaz.prikaziDodavanjeDetalja) {
      document.getElementById('osnovneButton')?.classList.add('selected');
    } else if (this.prikaz.prikaziDodavanjeSlika) {
      document.getElementById('slikeButton')?.classList.add('selected');
    } else if (this.prikaz.prikaziDodavanjeKompatibilnih) {
      document.getElementById('kompatibilniButton')?.classList.add('selected');
    }
  }


  kompatibilnostForma = new FormGroup({
    artikal1: new FormControl(''),
    artikal2: new FormControl('', [Validators.required, this.provjeri.bind(this)])

  });

  provjeri(control: AbstractControl): ValidationErrors | null {
    const artikal1ID = this.zadnjiID;
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


  dodajKompatibilne() {
    if (this.kompatibilnostForma.valid) {
      const formData = {
        artikal1: this.zadnjiID,
        artikal2: this.kompatibilnostForma.get('artikal2')?.value
      }
      let url = MojConfig.server_adresa + `/Kompatibilnost-Dodaj`;
      this.httpClient.post(url, formData).subscribe((res) => {
      })
      this.kompatibilnostForma.reset();
    }
  }

  zavrsi() {
    this.dialogService.openConfirmDialog("Da li želite nastaviti dodavanje artikala?").afterClosed().subscribe((x=>{
      if(x==true)
      {
        this.prikaziDodavanjeDetalja();
      }
      else{
        this.dialogRef.close();
      }
    }))
  }
}



