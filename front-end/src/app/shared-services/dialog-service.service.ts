import { Injectable } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {MatConfirmDialogComponent} from "../mat-confirm-dialog/mat-confirm-dialog.component";
import {MatOkDialogComponent} from "../mat-ok-dialog/mat-ok-dialog.component";

@Injectable({
  providedIn: 'root'
})
export class DialogServiceService {

  constructor(private dialog:MatDialog) { }
  openConfirmDialog(msg:string){
    return this.dialog.open(MatConfirmDialogComponent,{
      width:'390px',
      panelClass:'confirm-dialog-container',
      disableClose:true,
      position:{top:"10px"},
      data:{
        message:msg
      }
    });
  }
  openOkDialog(msg:string){
    return this.dialog.open(MatOkDialogComponent,{
      width:'390px',
      panelClass:'ok-dialog-container',
      disableClose:true,
      position:{top:"10px"},
      data:{
        message:msg
      }
    });
  }

}
