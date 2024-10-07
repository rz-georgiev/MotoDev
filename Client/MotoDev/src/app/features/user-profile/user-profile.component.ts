import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { ActivatedRoute, Route, RouterStateSnapshot } from '@angular/router';
import { UserEditorComponent } from '../users/components/user-editor/user-editor.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogActions, MatDialogClose, MatDialogContent } from '@angular/material/dialog';
import { AuthService } from '../auth/services/auth.service';
import { UserService } from '../users/services/user.service';
import { RoleService } from '../roles/services/role.service';
import { RepairShopService } from '../repair-shops/services/repair-shop.service';
import { first, forkJoin, of, switchMap } from 'rxjs';
import { RepairShopUserService } from '../repair-shop-users/services/repair-shop-user.service';
import { CustomFileUploaderComponent } from "../../shared/components/custom-file-uploader/custom-file-uploader.component";

@Component({
  selector: 'app-user-profile',
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
    MatButtonModule,
    CustomFileUploaderComponent
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent {

  public userFormGroup!: FormGroup;
  public isSubmitted!: boolean;
  public errorMessage!: string;
  public okMessage!: string;
  public isPasswordFieldEnabled!: boolean;
  public imageUrl!: string;

  constructor(private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private userService: UserService,
    private roleService: RoleService,
    private repairShopUserService: RepairShopUserService,
    private repairShopService: RepairShopService) {

    this.userFormGroup = formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.nullValidator],
      phoneNumber: ['', Validators.nullValidator],
      repairShop: ['', Validators.nullValidator],
      role: ['', Validators.nullValidator],
      imageUrl: ['', Validators.nullValidator]
    });

  }

  ngOnInit() {
    const tokenInfo = this.authService.getDecodedToken(localStorage.getItem('authToken') ?? "");
    const userId = tokenInfo.userId;

    this.userService.getById(userId).pipe(
      switchMap(user => {
        return this.repairShopUserService.getRepairShopsForCurrentUser().pipe(
          switchMap(repairShopUser => {
            const repairShop = this.repairShopService.getForSpecifiedIds(repairShopUser.result.map(x => x.repairShopId));
            const role = this.roleService.getById(user.result.roleId);
            return forkJoin([of(user), repairShop, role]);
          })
        )
      })
    ).subscribe(([user, repairShop, role]) => {
      const userData = user.result;
      this.userFormGroup.patchValue({
        firstName: userData.firstName,
        lastName: userData.lastName,
        username: userData.username,
        password: '',
        phoneNumber: userData.phoneNumber,
        repairShop: repairShop.result.map(x => x.name).join(', '),
        role: role.result.name,
        imageUrl: userData.imageUrl
      })
      this.imageUrl = userData.imageUrl;
    });
  }

  public onSubmit() {
    this.isSubmitted = true;
    if (this.userFormGroup.valid) {

      const { firstName,
        lastName,
        username,
        password,
        phoneNumber
      } = this.userFormGroup.value;

      this.userService.editUserMinimized({
        firstName: firstName,
        lastName: lastName,
        username: username,
        password: password,
        phoneNumber: phoneNumber

      }).subscribe(data => {
        if (!data.isOk) {
          this.errorMessage = data.message
        }
        else {
          this.okMessage = "Successfully saved changes"
          this.authService.refreshToken(data.result.refreshToken);
          this.authService.updateCurrentUserInfo();
        }
      });
    }
  }

  public onPasswordFieldCheckedChanged() {
    this.isPasswordFieldEnabled = !this.isPasswordFieldEnabled;
    if (!this.isPasswordFieldEnabled) {
      this.userFormGroup.get('password')?.reset();
    }
  }

  onFileSelected(formData: FormData) {
    this.userService.updateProfileImage(formData).subscribe(data => {
      this.imageUrl = data.result.imageUrl;
      this.authService.currentUser.imageUrl = this.imageUrl;
      this.authService.refreshToken(data.result.refreshToken);
      this.authService.updateCurrentUserInfo();
    })
  }
}
