import {Component, Inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogClose, MatDialogRef} from "@angular/material/dialog";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";

@Component({
  selector: 'app-mat-confirm-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogClose, MatIconModule, MatButtonModule],
  templateUrl: './mat-confirm-dialog.component.html',
  styleUrl: './mat-confirm-dialog.component.css'
})
export class MatConfirmDialogComponent {

  // @ts-ignore
  constructor(@Inject(MAT_DIALOG_DATA) public data,
              public dialogRef:MatDialogRef<MatConfirmDialogComponent>) {
  }

  closeDialog() {
    this.dialogRef.close(false);
  }
}
