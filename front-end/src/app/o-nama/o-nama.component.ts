import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink} from "@angular/router";
import {MyAuthService} from "../shared-services/MyAuthService";

@Component({
  selector: 'app-o-nama',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './o-nama.component.html',
  styleUrl: './o-nama.component.css'
})
export class ONamaComponent {

  constructor(public myAuth:MyAuthService) {
  }
}
