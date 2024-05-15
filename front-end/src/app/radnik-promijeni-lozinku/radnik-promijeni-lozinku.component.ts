import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatButtonModule} from "@angular/material/button";
import {MatDialog, MatDialogClose, MatDialogContent} from "@angular/material/dialog";
import {MatIconModule} from "@angular/material/icon";
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators
} from "@angular/forms";
import {MojConfig} from "../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {LozinkaGetResponse} from "../kupac-profil-edit/lozinka-get-response";
import {MyAuthService} from "../shared-services/MyAuthService";
import {RadnikLozinkaGet} from "./radnik-lozinka-get";

@Component({
  selector: 'app-radnik-promijeni-lozinku',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatDialogClose, MatDialogContent, MatIconModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './radnik-promijeni-lozinku.component.html',
  styleUrl: './radnik-promijeni-lozinku.component.css'
})
export class RadnikPromijeniLozinkuComponent implements OnInit {
  private validnaForma: boolean = false;

  @Output() updateProfila: EventEmitter<any> = new EventEmitter();

  constructor(private httpClient: HttpClient, private dialogService: DialogServiceService, private dialog: MatDialog,
              public myAuth: MyAuthService) {

  }

  loznkaEdit = new FormGroup({
    id: new FormControl('id'),
    lozinka: new FormControl(''),
    staraLoz: new FormControl(''),
    novaLoz: new FormControl(''),
    loz: new FormControl(''),
    slikaRadnika: new FormControl(''),
  });

  ngOnInit(): void {
    this.dohvatiLozinku();
  }

  editLozinka() {
    const formData = {
      id: this.loznkaEdit.get('id')?.value,
      lozinka: this.loznkaEdit.get('lozinka')?.value,
      slikaRadnika: this.vrijednost,
    };
    if (this.validnaForma) {
      let url = MojConfig.server_adresa + `/Radnik-LozinkaUpdate`;
      this.httpClient.post(url, formData).subscribe((x => {
        this.dialogService.openOkDialog("Lozinka uspješno editovana!").afterClosed().subscribe(res => {
          if (res == true) {
            this.dialog.closeAll();
          }
        });
      }))
    }
    const updated={
      id: this.loznkaEdit.get('id')?.value,
      lozinka: this.loznkaEdit.get('lozinka')?.value,
      slikaRadnika: this.vrijednost,
    }
    this.updateProfila.emit(updated);
  }

  lozinkaGet: RadnikLozinkaGet | null = null;

  dohvatiLozinku() {
    let url = MojConfig.server_adresa + `/Radnik-LozinkaGet?ID=${this.myAuth.getAuthorizationToken()?.korisnickiNalogID}`;
    this.httpClient.get<LozinkaGetResponse>(url).subscribe((x => {
      this.lozinkaGet = x;
      // @ts-ignore
      this.loznkaEdit = new FormGroup({
        id: new FormControl(x['id'], Validators.required),
        lozinka: new FormControl('', [Validators.required, Validators.minLength(4), this.trenutnaLozValidator.bind(this)]),
        staraLoz: new FormControl('', [Validators.required, this.staraLozValidator.bind(this)]),
        novaLoz: new FormControl('', [Validators.required, Validators.minLength(4), this.novaLozValidator.bind(this)]),
        loz: new FormControl(x['lozinka']),
        slikaRadnika: new FormControl(x['slikaRadnika'])
      });
      this.loznkaEdit.statusChanges.subscribe((status) => {
        this.validnaForma = status === 'VALID';

      });
    }))
  }

  staraLozValidator(control: AbstractControl): ValidationErrors | null {
    const staraLoz = control.value;
    const loz = this.loznkaEdit.get('loz')?.value;

    if (staraLoz === loz) {
      return null;
    } else {
      return {areEqual: true};
    }
  }

  novaLozValidator(control: AbstractControl): ValidationErrors | null {
    const novaLoz = control.value;
    const loz = this.loznkaEdit.get('loz')?.value;

    if (novaLoz !== loz) {
      return null;
    } else {
      return {areNotEqual: true};
    }
  }

  trenutnaLozValidator(control: AbstractControl): ValidationErrors | null {
    const novaLoz = control.value;
    const lozinka = this.loznkaEdit.get('novaLoz')?.value;

    if (novaLoz === lozinka) {
      return null;
    } else {
      return {areEqual: true};
    }
  }


  protected readonly MojConfig = MojConfig;

  vrijednost!: any;

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("slika-input").files[0];
    if (file && this.loznkaEdit) {
      var reader = new FileReader();
      reader.onload = () => {
        this.vrijednost = reader.result?.toString();


        this.lozinkaGet!.slikaRadnika = this.vrijednost;
      };
      reader.readAsDataURL(file);
    }
  }
}
