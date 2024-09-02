import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-modal',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule, 
    MatDialogActions,
    MatDialogContent],
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.css'
})
export class ConfirmationModalComponent {

  constructor(public dialogRef: MatDialogRef<ConfirmationModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { message: string }
  ) { }

  onNoClick(): any {
    this.dialogRef.close(false);
  }
  onYesClick() {
    this.dialogRef.close(true);
  }

}
