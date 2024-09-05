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
import { switchMap } from 'rxjs';
import { RoleOption } from '../../../../shared/consts/roleOption';
import { AuthService } from '../../../auth/services/auth.service';
import { RepairShopUserDto } from '../../../repair-shop-users/models/repairShopUser';
import { RepairShopUserService } from '../../../repair-shop-users/services/repair-shop-user.service';
import { RepairShopService } from '../../../repair-shops/services/repair-shop.service';
import { RoleService } from '../../../roles/services/role.service';
import { UserDto } from '../../models/userDto';
import { UserService } from '../../services/user.service';


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
  public isInEditMode: boolean = false;
  public isModifiedUserOwner: boolean = false;
  public errorMessage!: string;
  public userDto!: UserDto;
  public repairShopUser!: RepairShopUserDto;

  constructor(public dialogRef: MatDialogRef<UserEditorComponent>,
    @Inject(MAT_DIALOG_DATA) private passedData: any,
    private repairShopService: RepairShopService,
    private userService: UserService,
    private authService: AuthService,
    private roleService: RoleService,
    private repairShopUserService: RepairShopUserService,
    private formBuilder: FormBuilder
  ) {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      phoneNumber: ['', Validators.nullValidator],
      repairShopId: ['', Validators.required],
      roleId: ['', Validators.required],
    });
  }

  ngOnInit() {

    this.isInEditMode = this.passedData?.id > 0;

    if (this.isInEditMode) {
      this.repairShopUserService.getRepairShopUserById(this.passedData.id).pipe(
        switchMap(data => {
          this.repairShopUser = data.result;
          return this.userService.getById(this.repairShopUser.userId);
        })
      ).subscribe(data => {
        this.userDto = data.result;
        this.isModifiedUserOwner = this.userDto.roleId == RoleOption.Owner;

        this.registerForm.patchValue({
          firstName: this.userDto.firstName,
          lastName: this.userDto.lastName,
          email: this.userDto.email,
          username: this.userDto.username,
          phoneNumber: this.userDto.phoneNumber,
          repairShopId: this.repairShopUser.repairShopId,
          roleId: this.userDto.roleId
        });
      });

      this.registerForm.get('password')?.clearValidators();
    }

    this.repairShopService.getRepairShopsForSpecifiedOwner(this.authService.currentUserId).subscribe(data => {
      this.repairShops = data.result;
    });

    this.roleService.getAll().subscribe(data => {
      this.roles = data.result;
    });

  }

  onNoClick() {
    this.dialogRef.close(false);
  }

  onYesClick() {
    this.isSubmitted = true;
    if (this.registerForm.valid) {
      const form = this.registerForm.value;
      const user: UserDto = {
        repairShopUserId: this.passedData?.id,
        firstName: form.firstName,
        lastName: form.lastName,
        email: form.email,
        username: form.username,
        password: form.password,
        phoneNumber: form.phoneNumber,
        repairShopId: form.repairShopId,
        roleId: form.roleId,
      };

      this.userService.createUser(user).subscribe(data => {
        if (data.isOk) {
          this.dialogRef.close({
            id: this.passedData?.id ?? data.result.repairShopUserId, // for new users and editted users
            firstName: user.firstName,
            lastName: user.lastName,
            repairShop: this.repairShops.find(x => x.id === form.repairShopId)?.name,
            position: this.roles.find(x => x.id === form.roleId)?.name ?? 'Owner' // owners cannot edit their position
          });
        }
        else {
          this.errorMessage = data.message;
        }
      });
    }
  }
}
