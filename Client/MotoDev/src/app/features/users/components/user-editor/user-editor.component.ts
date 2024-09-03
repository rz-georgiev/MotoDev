import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogModule, MatDialogRef } from '@angular/material/dialog'
import { MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../../auth/services/auth.service';
import { RoleService } from '../../../roles/services/role.service';
import { RepairShopService } from '../../../repair-shops/services/repair-shop.service';


@Component({
  selector: 'app-user-editor',
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
  templateUrl: './user-editor.component.html',
  styleUrl: './user-editor.component.css'
})
export class UserEditorComponent {

  public registerForm!: FormGroup;
  public isSubmitted!: boolean;
  public repairShops!: any[];
  public roles!: any[];

  constructor(public dialogRef: MatDialogRef<UserEditorComponent>,
    private userService: UserService,
    private authService: AuthService,
    private roleService: RoleService,
    private repairShopService: RepairShopService,
    private formBuilder: FormBuilder
  ) {

    this.registerForm = formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.nullValidator],
      repairShop: ['', Validators.required],
      role: ['', Validators.required],
    });

  }

  ngOnInit() {
    this.repairShopService.getRepairShopsForSpecifiedOwner(this.authService.currentUserId).subscribe(data => {
      this.repairShops = data.result;
    });

    this.roleService.getAll().subscribe(data => {
      this.roles = data.result;
    });
  }

  onNoClick() {
    this.dialogRef.close(true);
  }
  onYesClick() {
    this.isSubmitted = true;
    
    if (this.registerForm.valid) {
      this.registerForm.value;
      this.dialogRef.close(false);
    }
    else {
      
    } 


  }
}
