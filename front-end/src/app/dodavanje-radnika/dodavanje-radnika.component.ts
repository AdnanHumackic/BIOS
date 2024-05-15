import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MatButtonModule} from "@angular/material/button";
import {MatDialog, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {MatIconModule} from "@angular/material/icon";
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule, ValidationErrors, Validators,
} from "@angular/forms";
import {MojConfig} from "../moj-config";
import {MyAuthService} from "../shared-services/MyAuthService";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {Router} from "@angular/router";
import {AdminGetByIDResponse} from "./admin-getbyid";
import {DrzavaGetAllResponseDrzava} from "./drzava-getall";
import {KupacGetbyid} from "../kupac-profil-edit/kupac-getbyid";
import {DrzavaGetAllResponse} from "../kupac-profil-edit/drzava-getall";
import {LozinkaGetResponse} from "../kupac-profil-edit/lozinka-get-response";
import {Prikaz} from "../kupac-profil-edit/prikaz";

@Component({
  selector: 'app-dodavanje-radnika',
  standalone: true,
  imports: [CommonModule, HttpClientModule, MatButtonModule, MatDialogClose, MatDialogContent, MatIconModule, ReactiveFormsModule],
  templateUrl: './dodavanje-radnika.component.html',
  styleUrl: './dodavanje-radnika.component.css'
})
export class DodavanjeRadnikaComponent implements OnInit{
  public isFormValid: boolean=false;
  private validnaForma: boolean=false;

  @Output() updateProfila: EventEmitter<any> = new EventEmitter();

  constructor(private httpClient:HttpClient, public myAuth:MyAuthService,
              private dialogService:DialogServiceService, private router:Router, private dialog:MatDialog) {
  }
  adminEditForm=new FormGroup({
    id: new FormControl(''),
    korisnickoIme: new FormControl(''),
    ime: new FormControl('',),
    prezime: new FormControl(''),
    drzava: new FormControl(''),
    slikaKorisnika:new FormControl(('SlikaKorisnika'))
  });

  admin:AdminGetByIDResponse | null=null;
  drzava:DrzavaGetAllResponseDrzava[]=[];
  ngOnInit(): void {
    this.dohvati();
    this.dohvatiDrzave();
    this.dohvatiLozinku();
  }
  dohvati() {
    let url=MojConfig.server_adresa+`/Admin-GetByID?ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalog.id}`;
    this.httpClient.get<AdminGetByIDResponse>(url).subscribe((x=>{
      this.admin=x;
      // @ts-ignore
      this.adminEditForm=new FormGroup({
        id: new FormControl(x['id']),
        korisnickoIme: new FormControl(x['korisnickoIme'], [Validators.required, Validators.minLength(5)]),
        ime: new FormControl(x['ime'] ,[Validators.required, Validators.pattern(/^[A-Z][a-z]*$/)]),
        prezime: new FormControl(x['prezime'], [Validators.required, Validators.pattern(/^[A-Z][a-z]*$/)]),
        drzava: new FormControl(x['drzava']),
        slikaKorisnika:new FormControl(x['slikaKorisnika'])
      });
      console.log(this.adminEditForm);
      this.adminEditForm.statusChanges.subscribe((status) => {
        this.isFormValid = status === 'VALID';

      });
    }));
  }

  dohvatiDrzave() {
    let url=MojConfig.server_adresa+`/Drzava-GetAll`;
    this.httpClient.get<DrzavaGetAllResponse>(url).subscribe((x=>{
      this.drzava=x.drzava;
    }));
  }

  updatePodataka() {

    const formData = {
      id:this.adminEditForm.get('id')?.value,
      korisnickoIme: this.adminEditForm.get('korisnickoIme')?.value,
      ime: this.adminEditForm.get('ime')?.value,
      prezime: this.adminEditForm.get('prezime')?.value,
      drzava: this.adminEditForm.get('drzava')?.value,
      slikaKorisnika:this.vrijednost
    };

    if(this.adminEditForm.valid){
      let url=MojConfig.server_adresa+`/Admin-Update`;
      this.httpClient.post(url, formData).subscribe((x)=>{
        console.log(formData);
      })
      this.dialogService.openOkDialog("Korisnički nalog uspješno editovan!").afterClosed().subscribe(res => {
        if (res == true) {
          this.dialog.closeAll();
        }
      });
    }else if(this.adminEditForm.invalid){
      this.dialogService.openOkDialog("Molimo unesite validne podatke!");
    }

    const updated={
      id:this.adminEditForm.get('id')?.value,
      korisnickoIme: this.adminEditForm.get('korisnickoIme')?.value,
      ime: this.adminEditForm.get('ime')?.value,
      prezime: this.adminEditForm.get('prezime')?.value,
      drzava: this.adminEditForm.get('drzava')?.value,
      slikaKorisnika:this.vrijednost,
      email: this.adminEditForm.get('email')?.value
    }
    this.updateProfila.emit(updated);
  }

  // @ts-ignore
  loznkaEdit= new FormGroup({
    lozinka: new FormControl(''),
    staraLoz:new FormControl(''),
    novaLoz:new FormControl(''),
    loz:new FormControl('')
  });
  staraLozValidator(control: AbstractControl): ValidationErrors | null {
    const staraLoz = control.value;
    const loz = this.loznkaEdit.get('loz')?.value;

    if (staraLoz === loz) {
      return null;
    } else {
      return { areEqual: true };
    }
  }
  novaLozValidator(control: AbstractControl): ValidationErrors | null {
    const novaLoz = control.value;
    const loz = this.loznkaEdit.get('loz')?.value;

    if (novaLoz !== loz) {
      return null;
    } else {
      return { areNotEqual: true };
    }
  }
  trenutnaLozValidator(control: AbstractControl): ValidationErrors | null {
    const novaLoz = control.value;
    const lozinka = this.loznkaEdit.get('novaLoz')?.value;

    if (novaLoz === lozinka) {
      return null;
    } else {
      return { areEqual: true };
    }
  }
  editLozinka() {
    if(this.validnaForma){
      let url=MojConfig.server_adresa+`/Admin-LozinkaUpdate`;
      this.httpClient.post(url, this.loznkaEdit.value).subscribe((x=>{
        this.dialogService.openOkDialog("Lozinka uspješno editovana!").afterClosed().subscribe(res => {
          if (res == true) {
            this.dialog.closeAll();
          }
        });
      }))
    }
  }
lozinkaGet!:LozinkaGetResponse;
  dohvatiLozinku() {
    let url=MojConfig.server_adresa+`/Admin-LozinkaGet?ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalogID}`;
    this.httpClient.get<LozinkaGetResponse>(url).subscribe((x=>{
      this.lozinkaGet=x;
      // @ts-ignore
      this.loznkaEdit=new FormGroup({
        id: new FormControl(x['id'], Validators.required),
        //x['lozinka']
        lozinka: new FormControl('',[Validators.required, Validators.minLength(4), this.trenutnaLozValidator.bind(this)] ),
        staraLoz: new FormControl('', [Validators.required,  this.staraLozValidator.bind(this)]),
        novaLoz: new FormControl('', [Validators.required, Validators.minLength(4), this.novaLozValidator.bind(this)]),
        loz:new FormControl(x['lozinka'])
      });
      this.loznkaEdit.statusChanges.subscribe((status) => {
        this.validnaForma = status === 'VALID';
      });
    }))
  }
  prikaz: Prikaz = { prikaziUredjivanjeProfila: true, prikaziPromjenuSifre: false };
  prikaziUredjivanjeProfila(event: any) {
    this.prikaz.prikaziUredjivanjeProfila=true;
    this.prikaz.prikaziPromjenuSifre=false;
    let vr = document.getElementById("novaSl");
    if (vr) {
      vr.style.display = "block";
    }
    this.Odaberi(event);
  }
  Odaberi(event: any) {
    let buttons = document.querySelectorAll('.navbarButton');
    buttons.forEach(element=>
    {
      element.classList.remove('selected');
      element.classList.add('not_selected');
    });
    event.target.classList.add('selected');
  }
  prikaziPromjenuSifre(event: any) {
    this.prikaz.prikaziUredjivanjeProfila=false;
    this.prikaz.prikaziPromjenuSifre=true;
    let vr = document.getElementById("novaSl");
    if (vr) {
      vr.style.display = "none";
    }
    this.Odaberi(event);
  }
  protected readonly MojConfig = MojConfig;
  vrijednost!:any;

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("slika-input").files[0];
    if (file && this.adminEditForm) {
      var reader = new FileReader();
      reader.onload = () => {
        this.vrijednost = reader.result?.toString();


        this.admin!.slikaKorisnika = this.vrijednost;
      };
      reader.readAsDataURL(file);
    }
  }



}
