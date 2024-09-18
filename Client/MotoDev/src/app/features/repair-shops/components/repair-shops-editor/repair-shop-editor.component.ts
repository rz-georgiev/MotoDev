import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { UserEditorComponent } from '../../../users/components/user-editor/user-editor.component';
import { RepairShopService } from '../../services/repair-shop.service';
import { RepairShopDto } from '../../models/repairShopDto';

@Component({
  selector: 'app-repair-shop-editor',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    UserEditorComponent,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatButtonModule
  ],
  templateUrl: './repair-shop-editor.component.html',
  styleUrl: './repair-shop-editor.component.css'
})
export class RepairShopEditorComponent {
  repairShopForm!: FormGroup;
  isSubmitted!: boolean;
  isInEditMode!: boolean;
  errorMessage!: string;

  constructor(private dialogRef: MatDialogRef<RepairShopEditorComponent>,
    @Inject(MAT_DIALOG_DATA) private passedData: any,
    private formBuilder: FormBuilder,
    private repairShopService: RepairShopService
  ) { }

  ngOnInit() {

    this.repairShopForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      address: ['', [Validators.required]],
      city: ['', [Validators.required]],
      email: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      vatNumber: ['', [Validators.required]],
    });

    if (this.passedData.id > 0) {
      const detail = this.repairShopService.getById(this.passedData.id).subscribe(data => {
        this.repairShopForm.patchValue({
          name: data.result.name,
          address: data.result.address,
          city: data.result.city,
          email: data.result.email,
          phoneNumber: data.result.phoneNumber,
          vatNumber: data.result.vatNumber,
        });

        this.isInEditMode = true;
      });
    }

  }

  onNoClick() {
    this.dialogRef.close(null);
  }

  onYesClick() {
    this.isSubmitted = true;

    if (this.repairShopForm.invalid)
      return;
    
    const formValues = this.repairShopForm.value as RepairShopDto
    if (this.passedData?.id) {
      formValues.id = this.passedData.id;
    }
   
    this.repairShopService.edit(formValues).subscribe(data => {
      this.dialogRef.close(data);
    }); 
  }
}
