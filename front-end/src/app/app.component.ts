import {Component, OnDestroy, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {NavBarComponent} from "./nav-bar/nav-bar.component";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {SignalRService} from "./shared-services/signalR.service";
import {MyAuthService} from "./shared-services/MyAuthService";
import {TokenRefreshService} from "./shared-services/token-refresh-service";
import {interval, Subscription, takeWhile} from "rxjs";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, HttpClientModule, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'front-end';

  constructor(private signalRService:SignalRService, private tokenRefreshService: TokenRefreshService, private myAuth:MyAuthService) {
  }

  private refreshSubscription: Subscription | undefined;

  ngOnInit(): void {
    this.signalRService.otvori_ws_konekciju();

    const oldToken = this.myAuth.getAuthorizationToken()?.vrijednost;
    if (oldToken) {
      this.refreshSubscription = interval(45 * 60 * 1000)
        .pipe(
          takeWhile(() => this.myAuth.isLogiran())
        )
        .subscribe(() => {
          this.tokenRefreshService.refreshAuthToken(oldToken).subscribe();
        });
    }
  }
  ngOnDestroy(): void {
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
    }
  }
}
