import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CommonModule, DOCUMENT} from '@angular/common';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule, isFormControl,
  ReactiveFormsModule, ValidationErrors,
  ValidatorFn,
  Validators
} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatDialog, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {MatIconModule} from "@angular/material/icon";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {KupacGetbyid} from "./kupac-getbyid";
import {MojConfig} from "../moj-config";
import {MyAuthService} from "../shared-services/MyAuthService";
import {DrzavaGetAllResponse, DrzavaGetAllResponseDrzava} from "./drzava-getall";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {Router} from "@angular/router";
import {FormBuilder} from "@angular/forms";
import {LozinkaGetResponse} from "./lozinka-get-response";
import {Prikaz} from "./prikaz";

@Component({
  selector: 'app-kupac-profil-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule, MatDialogClose, MatIconModule, MatDialogContent, HttpClientModule, ReactiveFormsModule],
  templateUrl: './kupac-profil-edit.component.html',
  styleUrl: './kupac-profil-edit.component.css'
})
export class KupacProfilEditComponent implements OnInit {
  public isFormValid: boolean=false;
  private validnaForma: boolean=false;

  @Output() updateProfila: EventEmitter<any> = new EventEmitter();

  constructor(private httpClient:HttpClient, public myAuth:MyAuthService,
              private dialogService:DialogServiceService, private router:Router, private dialog:MatDialog) {

  }


  kupacEditForm=new FormGroup({
    id: new FormControl(''),
    korisnickoIme: new FormControl(''),
    ime: new FormControl('',),
    prezime: new FormControl(''),
    drzava: new FormControl(''),
    slikaKorisnika:new FormControl(('slikaKorisnika')),
    email: new FormControl('')
  });





  kupac:KupacGetbyid | null=null;
  drzava:DrzavaGetAllResponseDrzava[]=[];



  ngOnInit(): void {
    this.dohvati();
    this.dohvatiDrzave();
    this.dohvatiLozinku();
  }


  dohvati() {
    let url=MojConfig.server_adresa+`/Kupac-GetByID?ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalogID}`;
    this.httpClient.get<KupacGetbyid>(url).subscribe((x=>{
      this.kupac=x;
      // @ts-ignore
      this.kupacEditForm=new FormGroup({
        id: new FormControl(x['id']),
        korisnickoIme: new FormControl(x['korisnickoIme'], [Validators.required, Validators.minLength(5)]),
        ime: new FormControl(x['ime'] ,[Validators.required, Validators.pattern(/^[A-Z][a-z]*$/)]),
        prezime: new FormControl(x['prezime'], [Validators.required, Validators.pattern(/^[A-Z][a-z]*$/)]),
        drzava: new FormControl(x['drzava']),
        slikaKorisnika:new FormControl(x['slikaKorisnika']),
        email: new FormControl(x['email'], [Validators.required, Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)])
      });
      this.kupacEditForm.statusChanges.subscribe((status) => {
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
      id:this.kupacEditForm.get('id')?.value,
      korisnickoIme: this.kupacEditForm.get('korisnickoIme')?.value,
      ime: this.kupacEditForm.get('ime')?.value,
      prezime: this.kupacEditForm.get('prezime')?.value,
      drzava: this.kupacEditForm.get('drzava')?.value,
      slikaKorisnika:this.vrijednost,
      email: this.kupacEditForm.get('email')?.value
    };

    if(this.isFormValid){
      let url=MojConfig.server_adresa+`/Kupac-Update`;
      this.httpClient.post(url, formData).subscribe((x)=>{
        console.log(formData);
      })
      this.dialogService.openOkDialog("Korisnički nalog uspješno editovan!").afterClosed().subscribe(res => {
        if (res == true) {
          this.dialog.closeAll();
          this.router.navigate(['/pregledKomp']);
        }
      });
    }

    const updated={
      id:this.kupacEditForm.get('id')?.value,
      korisnickoIme: this.kupacEditForm.get('korisnickoIme')?.value,
      ime: this.kupacEditForm.get('ime')?.value,
      prezime: this.kupacEditForm.get('prezime')?.value,
      drzava: this.kupacEditForm.get('drzava')?.value,
      slikaKorisnika:this.vrijednost,
      email: this.kupacEditForm.get('email')?.value
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
      let url=MojConfig.server_adresa+`/Kupac-LozinkaUpdate`;
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
    let url=MojConfig.server_adresa+`/Kupac-LozinkaGet?ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalogID}`;
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
   /* // @ts-ignore
    var file = document.getElementById("slika-input").files[0];
    if (file && this.kupacEditForm)
    {
      var reader = new FileReader();
      reader.onload = ()=>{
        this.kupac!.slikaKorisnika = reader.result?.toString();
      }
      reader.readAsDataURL(file)
    }*/
    // @ts-ignore
    var file = document.getElementById("slika-input").files[0];
    if (file && this.kupacEditForm) {
      var reader = new FileReader();
      reader.onload = () => {
        this.vrijednost = reader.result?.toString();


        this.kupac!.slikaKorisnika = this.vrijednost;
      };
      reader.readAsDataURL(file);
    }
  }
}
