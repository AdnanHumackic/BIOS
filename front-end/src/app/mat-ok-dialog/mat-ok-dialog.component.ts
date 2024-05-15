import {Component, Inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MAT_DIALOG_DATA, MatDialogClose, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-mat-ok-dialog',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogClose],
  templateUrl: './mat-ok-dialog.component.html',
  styleUrl: './mat-ok-dialog.component.css'
})
export class MatOkDialogComponent {

  // @ts-ignore
  constructor(@Inject(MAT_DIALOG_DATA) public data,
              public dialogRef:MatDialogRef<MatOkDialogComponent>) {
  }
  closeDialog() {
    this.dialogRef.close(false);
  }
}
