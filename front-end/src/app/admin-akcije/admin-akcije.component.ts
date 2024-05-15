import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule, ValidationErrors, ValidatorFn,
  Validators
} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatDialog, MatDialogActions, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {MatIconModule} from "@angular/material/icon";
import {StavkeRadnikResponse, StavkeRadnikResponseStavke} from "../zadaci/radnik-GetAll";
import {StavkeZadatakResponse, StavkeZadatakResponseStavke} from "../zadaci/zadaciGetAll";
import {Subject} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {DrzavaGetAllResponseDrzava} from "../dodavanje-radnika/drzava-getall";
import {ZadaciGetByIdResponse} from "../zadaci/Zadaci-GetById";
import {DrzavaGetAllResponse} from "../kupac-profil-edit/drzava-getall";
import {PrikaziAdmin} from "../dodavanje-radnika/prikazi-admin";
import {MojConfig} from "../moj-config";
import {MyAuthService} from "../shared-services/MyAuthService";
import {Korisnici, Profili} from "./profiliGetAll";
import {MatRadioModule} from "@angular/material/radio";


export interface ZadaciRequest {
  id: number;
  naziv: string;
  opis: string;
  datumPocetka: string;
  datumZavrsetka: string;
  radnikId: number;
}
@Component({
  selector: 'app-admin-akcije',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatDialogClose, MatDialogContent, MatIconModule, ReactiveFormsModule, MatRadioModule, MatDialogActions],
  templateUrl: './admin-akcije.component.html',
  styleUrl: './admin-akcije.component.css'
})
export class AdminAkcijeComponent implements OnInit {
  profili: Korisnici[]=[];
  selectedUser: Korisnici | undefined;
  selectedRole: string | undefined;
  showEditDialog: boolean = false;
  disableRadioButtons: boolean = false;

  //public isFormValid: boolean=false;
  zadaciRequest: ZadaciRequest = {
    id: 0,
    naziv: '',
    opis: '',
    datumPocetka: '',
    datumZavrsetka: '',
    radnikId: 0
  };
  radnik: StavkeRadnikResponseStavke[] = [];
  // @ts-ignore
  zadaciForm: FormGroup;
  zadaci: StavkeZadatakResponseStavke[] = [];
  private refreshZadaci$ = new Subject<void>();
  constructor(public httpClient:HttpClient,
              public dialogService:DialogServiceService,
              public dialog:MatDialog,
              public myAuth: MyAuthService,
              private fb: FormBuilder) {
  }

  drzava:DrzavaGetAllResponseDrzava[]=[];
  dodavanjeRadnika=new FormGroup({
    korisnickoIme: new FormControl('', [Validators.required, Validators.minLength(5)]),
    ime: new FormControl('', [Validators.required, Validators.pattern(/^[A-Z][a-z]*$/)]),
    prezime: new FormControl('', [Validators.required, Validators.pattern(/^[A-Z][a-z]*$/)]),
    drzava: new FormControl('', Validators.required),
    lozinka: new FormControl('', [Validators.required, Validators.minLength(4)]),
    datumRodjenja: new FormControl('', [Validators.required,  this.datumValidator.bind(this)]),
    email: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)])
  });
  datumValidator(control: AbstractControl): ValidationErrors | null {
    const selectedDate = control.value ? new Date(control.value) : null;
    const currentDate = new Date();

    if (selectedDate && selectedDate > currentDate) {
      return { 'maxDate': true };
    }

    return null;

  }

  ngOnInit(): void {
    this.ucitajDrzave();
    this.createForm();
    this.fetchRadnike();
    this.fetchZadaci();
    this.refreshZadaci$.subscribe(() => {
      this.fetchZadaci();
    });
    this.fetchKorisnici();
  }
  private createForm() {
    this.zadaciForm = this.fb.group({
      naziv: ['', Validators.compose([Validators.required])],
      opis: ['', Validators.compose([Validators.required])],
      datumPocetka: ['', Validators.compose([Validators.required])],
      datumZavrsetka: ['', Validators.compose([Validators.required, this.datumZadatakValidator])],
      radnikId: [0, Validators.required]
    });
  }
  private datumZadatakValidator(): ValidatorFn {
    return (control: { value: string }) => {
      const inputDate = new Date(control.value);
      const currentDate = new Date();
      return inputDate >= currentDate ? null : { dateInFuture: true };
    };
  }

  private fetchRadnike() {
    const radnikUrl = MojConfig.server_adresa + `/Radnik-GetAll`;
    this.httpClient.get<StavkeRadnikResponse>(radnikUrl).subscribe((data) => {
      this.radnik = data.radnik;
    });
  }
  private fetchZadaci() {
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
  Dodaj() {
    if (this.zadaciForm.invalid) {
      this.dialogService.openOkDialog("Molimo unesite sve potrebne informacije!");
      return;
    }
    this.zadaciRequest = this.zadaciForm.value;
    const zadaciUrl = MojConfig.server_adresa + `/Zadatak-Dodaj`;

    this.httpClient.post(zadaciUrl, this.zadaciRequest).subscribe((response: any): void => {
      this.dialogService.openOkDialog("Dodavanje je uspjesno!");
      this.refreshZadaci$.next();
      this.zadaciForm.reset();
    });
  }
  protected readonly MyAuthService = MyAuthService;

  Obrisi(id: number) {
    const ZadUrl=MojConfig.server_adresa+`/Zadaci-Obrisi?ID=${id}`;
    this.httpClient.delete(ZadUrl).subscribe((res:any):void=>{
      this.dialogService.openOkDialog("Zadatak uspjesno odradjen!");
      this.ngOnInit();
    })
  }
  ucitajDrzave() {
    let url=MojConfig.server_adresa+`/Drzava-GetAll`;
    this.httpClient.get<DrzavaGetAllResponse>(url).subscribe((x=>{
      this.drzava=x.drzava;
    }));
  }
  dodajRadnika() {

    /*this.dodavanjeRadnika.statusChanges.subscribe((status) => {
      this.isFormValid = status === 'VALID';

    });*/
    if(this.dodavanjeRadnika.valid){
      const formData = {
        korisnickoIme: this.dodavanjeRadnika.get('korisnickoIme')?.value,
        ime: this.dodavanjeRadnika.get('ime')?.value,
        prezime: this.dodavanjeRadnika.get('prezime')?.value,
        drzava: this.dodavanjeRadnika.get('drzava')?.value,
        lozinka: this.dodavanjeRadnika.get('lozinka')?.value,
        datumRodjenja: this.dodavanjeRadnika.get('datumRodjenja')?.value,
        email: this.dodavanjeRadnika.get('datumRodjenja')?.value,
      };
      let url=MojConfig.server_adresa+`/Admin-DodajRadnika`;
      this.httpClient.post(url,formData ).subscribe((x=>{
        console.log('uspjesno');
        this.dialogService.openOkDialog("Radnik uspješno dodan!").afterClosed().subscribe(res => {
          if (res == true) {
            this.dodavanjeRadnika.reset();
            this.dialog.closeAll();
          }
        });
      }))
    }
    else if(this.dodavanjeRadnika.invalid){
     this.dialogService.openOkDialog("Molimo unesite validne podatke!");
    }
  }

  generisiLozinku() {
    const minLength = 6;
    const maxLength = 12;
    const length = Math.floor(Math.random() * (maxLength - minLength + 1)) + minLength;
    const charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    let sifra = "";

    for (let i = 0; i < length; i++) {
      const randomIndex = Math.floor(Math.random() * charset.length);
      sifra += charset[randomIndex];
    }
    //???
    // @ts-ignore
    this.dodavanjeRadnika.get('lozinka').value=sifra;
    // @ts-ignore
    this.dodavanjeRadnika.get('lozinka').setValue(sifra);

    // @ts-ignore
    document.getElementById("lozinka").innerText= this.dodavanjeRadnika.get('lozinka').setValue(sifra);
  }
  prikaz: PrikaziAdmin = { dodavanjeRadnika: true, editovanjeProfila: false, upravljanjeKorisnickimNalozima:false, aktivniZadaci:false, dodavanjeDokumenata:false};

  prikaziDodavanjeRadnika(event: any) {
    this.prikaz.dodavanjeRadnika=true;
    this.prikaz.editovanjeProfila=false;
    this.prikaz.upravljanjeKorisnickimNalozima=false;
    this.prikaz.aktivniZadaci=false;
    this.prikaz.dodavanjeDokumenata=false;
    this.odaberi(event)
  }

  prikaziEditovanjeProfila(event: any) {
    this.prikaz.dodavanjeRadnika=false;
    this.prikaz.editovanjeProfila=true;
    this.prikaz.upravljanjeKorisnickimNalozima=false;
    this.prikaz.aktivniZadaci=false;
    this.prikaz.dodavanjeDokumenata=false;
    this.odaberi(event);
  }

  prikaziUpravljanjeKorisnickim(event: any) {
    this.prikaz.dodavanjeRadnika=false;
    this.prikaz.editovanjeProfila=false;
    this.prikaz.upravljanjeKorisnickimNalozima=true;
    this.prikaz.aktivniZadaci=false;
    this.prikaz.dodavanjeDokumenata=false;
    this.odaberi(event);
  }
  prikaziAktivneZadatke(event: any) {
    this.prikaz.dodavanjeRadnika=false;
    this.prikaz.editovanjeProfila=false;
    this.prikaz.upravljanjeKorisnickimNalozima=false;
    this.prikaz.aktivniZadaci=true;
    this.prikaz.dodavanjeDokumenata=false;
    this.odaberi(event);
  }
  prikaziDodavanjeDokumenata(event: MouseEvent) {
    this.prikaz.dodavanjeRadnika=false;
    this.prikaz.editovanjeProfila=false;
    this.prikaz.upravljanjeKorisnickimNalozima=false;
    this.prikaz.aktivniZadaci=false;
    this.prikaz.dodavanjeDokumenata=true;
    this.odaberi(event);
  }
  odaberi(event: any) {
    let buttons = document.querySelectorAll('.navbarButton');
    buttons.forEach(element=>
    {
      element.classList.remove('selected');
      element.classList.add('not_selected');
    });
    event.target.classList.add('selected');
  }

  protected readonly MojConfig = MojConfig;

  private fetchKorisnici() {
    const korisniciUrl = MojConfig.server_adresa + `/KorisniciGetAll-KorisniciGetAll`;
    this.httpClient.get<Profili>(korisniciUrl).subscribe((data) => {
      this.profili = data.korisnici;
    });
  }
  obrisiProfil(id: number) {
    const obrisiUrl = MojConfig.server_adresa + `/Profil-Obrisi?ID=${id}`;
    this.dialogService.openConfirmDialog("Deaktivirati profil?").afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.httpClient.delete(obrisiUrl).subscribe(
          (response: any) => {
            this.dialogService.openOkDialog("Profil uspjesno obrisan!");
            this.ngOnInit();
          },
          (error: any) => {
            console.error('Došlo je do greške prilikom brisanja profila', error);
          }
        );
      } else {
        console.log("Brisanje profila otkazano.");
      }
    });
  }

  openEditDialog(profil: Korisnici) {
    if(this.myAuth.getAuthorizationToken()?.korisnickiNalogID === profil.id){
      this.dialogService.openOkDialog("Ne možete promeniti ulogu trenutno ulogovanom korisniku!");
      return;
    }

    this.selectedUser = profil;
    this.showEditDialog = true;
    this.disableRadioButtons = false;
  }

  closeDialog() {
    this.showEditDialog = false;
  }

  saveChanges() {
    this.disableRadioButtons = true;
    setTimeout(() => {
      this.showEditDialog = false;
      this.disableRadioButtons = false;
      this.dialogService.openOkDialog("Uloga uspjesno promijenjena!");
    }, 1000);
  }



  public selectedFiles: File[] = [];
  public fileUrl: string = "";

  handleFileInput(event: Event) {
    let files = (event.target as HTMLInputElement).files;
    if (files) {
      this.fileUrl = window.URL.createObjectURL(files[0]);

      for (let index = 0; index < files.length; index++) {
        if (files.item(index)) {
          this.addFileToQueue(files.item(index) as File);
        }
      }
    }
  }
  private addFileToQueue(file: File) {
    this.selectedFiles.push(file);
  }
  file: any = null;

  sendData() {
    this.sendFile(this.selectedFiles);
    const uploadInput = document.getElementById('upload') as HTMLInputElement;
    // @ts-ignore
    uploadInput.value = null;
  }
  sendFile(files: File[]) {
    if (files) {
      for (let index = 0; index < files.length; index++) {
        if (files[index]) {
          this.postFile(files[index]);
        }
      }
    }
    this.selectedFiles = [];

  }

  postFile(fileToUpload:File){
    const formData=new FormData();
    formData.append('File', fileToUpload);

    const headers=new HttpHeaders().append('Content-Disposition', 'multipart/form-data'); //Disposition
    let url=MojConfig.server_adresa+`/controller`;

    this.httpClient.post(url, formData, {headers}).subscribe(x=>{
      this.dialogService.openOkDialog("Uspješno dodan dokument!").afterClosed().subscribe(x=>{
      });
    })
  }

  prebaciUAdmin(id: number) {
    const url = MojConfig.server_adresa + '/Uloga-Admin';
    this.httpClient.post(url, { ID: id }).subscribe(
      (response: any) => {
        this.dialogService.openOkDialog("Uloga uspjesno promjenjena!");
        this.ngOnInit();
        this.closeDialog();
      },
      (error: any) => {
        this.dialogService.openOkDialog("Došlo je do greške prilikom prebacivanja u admina!");
      }
    );
  }

  prebaciURadnika(id: number) {
    const url = MojConfig.server_adresa + '/Uloga-Radnik';
    this.httpClient.post(url, { ID: id }).subscribe(
      (response: any) => {
        this.dialogService.openOkDialog("Uloga uspjesno promjenjena!");
        this.ngOnInit();
        this.closeDialog();
      },
      (error: any) => {
        this.dialogService.openOkDialog("Došlo je do greške prilikom prebacivanja u radnika!");
      }
    );
  }

  prebaciUKupca(id: number) {
    const url = MojConfig.server_adresa + '/Uloga-Kupac';
    this.httpClient.post(url, { ID: id }).subscribe(
      (response: any) => {
        this.dialogService.openOkDialog("Uloga uspjesno promjenjena!");
        this.ngOnInit();
        this.closeDialog();
      },
      (error: any) => {
        this.dialogService.openOkDialog("Došlo je do greške prilikom prebacivanja u kupca!");
      }
    );
  }
}
