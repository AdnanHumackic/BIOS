import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import{HttpClient} from '@angular/common/http';
import {MojConfig} from "../moj-config";
import {StavkeWishlistResponse, StavkeWishlistResponseStavke} from "./wishlist-GetAll";
import {WishlistGetByIdResponse, WishlistGetByIdResponseWishlist} from "./wishlist-GetByID";
import {MyAuthService} from "../shared-services/MyAuthService";
import {DialogServiceService} from "../shared-services/dialog-service.service";
import {Artikli} from "../upravljanje-artiklima/get-all-koji-nisu-obrisni-paged-response";
import {DataItemObrisani} from "../upravljanje-artiklima/get-all-koji-su-obrisani-paged-response";
import {DataItemWishlist, WishlistPaged} from "./wishlist-get-by-id-paged";
@Component({
  selector: 'app-wishlist-upravljanje',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './wishlist-upravljanje.component.html',
  styleUrl: './wishlist-upravljanje.component.css'
})

export class WishlistUpravljanjeComponent implements OnInit{
  wishlistItems: WishlistItem[]=[];
  constructor(private http: HttpClient, public MyAuth:MyAuthService,
              private dialogService:DialogServiceService) {}
  ngOnInit() {
    //this.fetchWishlistItems();
    this.loadArtikliPaged(this.currentPage);
  }

  wishlist:WishlistGetByIdResponseWishlist[]=[];
  /*fetchWishlistItems() {
    const wishlistUrl=MojConfig.server_adresa+`/Wishlist-GetByID?ID=${this.MyAuth.getAuthorizationToken()?.korisnickiNalogID}`;
    this.http.get<WishlistGetByIdResponse>(wishlistUrl).subscribe((data)=>{
      this.wishlist=data.wishlist;
    });
  }*/

  wish:DataItemWishlist[]=[];
  currentPage: number = 1;
  totalPages: number = 1;
  loadArtikliPaged(pageNumber: number) {

    const artPoStranici = 3;
    const urlArt2 = MojConfig.server_adresa+`/Wishlist-GetByIDPaged?PageSize=${artPoStranici}&PageNumber=${pageNumber}&ID=${this.MyAuth.getAuthorizationToken()?.korisnickiNalogID}`;

    this.http.get<WishlistPaged>(urlArt2).subscribe((x: WishlistPaged) => {
      this.wish = x.wishlist.dataItems;
      this.currentPage = x.wishlist.currentPage;
      this.totalPages = x.wishlist.totalPages;

      this.provjeriJelPrazna();
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
    return this.jelPrazna(this.wish);
  }

  jelPrazna(art: DataItemWishlist[]): boolean {
    return art.length === 0;
  }
  obrisiIzWishlista(id:number){
    let url=MojConfig.server_adresa+`/Wishlist-Obrisi?ID=${id}`;

    this.http.delete(url).subscribe((res:any)=>{
      this.dialogService.openOkDialog('Artikal uspješno obrisan iz wishlista!');
      this.ngOnInit();
    })
  }

  protected readonly MojConfig = MojConfig;
}
export interface WishlistItem{
  ImeArtikla:string;
  Proizvodjac:string;
  Cijena:number;
  Opis:string;
  DatumDodavanja:Date;
}
