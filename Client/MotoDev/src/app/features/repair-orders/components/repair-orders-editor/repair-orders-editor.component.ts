import { CommonModule } from '@angular/common';
import { Component, Inject, ViewChild } from '@angular/core';
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
import { UserDto } from '../../../users/models/userDto';
import { UserService } from '../../../users/services/user.service';
import { CarRepairService } from '../../services/car.repair.service';
import { ClientResponse } from '../../models/clientResponse';
import { SelectFilterComponent } from "../../../../shared/components/select-filter/select-filter.component";
import { ClientCarResponse } from '../../models/clientCarResponse';
import { ClientService } from '../../../clients/services/client.service';
import { ClientCarService } from '../../../client-cars/services/client-car.service';
import { CarRepairRequest } from '../../models/carRepairRequest';
import { UtcToLocalPipe } from '../../../../core/pipes/utc-to-local.pipe';

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
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatButtonModule,
    SelectFilterComponent
  ],
  templateUrl: './repair-orders-editor.component.html',
  styleUrl: './repair-orders-editor.component.css'
})
export class RepairOrdersEditorComponent {

  public repairOrderForm!: FormGroup;
  public clients!: ClientResponse[];
  public clientCars!: ClientCarResponse[];
  public isSubmitted!: boolean;
  public isInEditMode: boolean = false;
  public errorMessage!: string;

  constructor(public dialogRef: MatDialogRef<RepairOrdersEditorComponent>,
    @Inject(MAT_DIALOG_DATA) private passedData: any,
    private repairShopService: RepairShopService,
    private userService: UserService,
    private authService: AuthService,
    private roleService: RoleService,
    private carRepairService: CarRepairService,
    private clientService: ClientService,
    private clientCarService: ClientCarService,
    private formBuilder: FormBuilder
  ) {
    this.repairOrderForm = this.formBuilder.group({
      clientId: ['', Validators.required],
      clientCarId: ['', Validators.required]
    });
  }

  ngOnInit() {

    this.isInEditMode = this.passedData?.carRepairId > 0;
    this.clientService.getClients().subscribe(x => {
      this.clients = x.result;
    });

    // this.carRepairService.getById(passedData?.carRepairId)
    // if (this.isInEditMode) {
    //   this.carRepairService.getRepairShopUserById(this.passedData.repairShopUserId).pipe(
    //     switchMap(data => {
    //       this.repairShopUser = data.result;
    //       return this.userService.getById(this.repairShopUser.userId);
    //     })
    //   ).subscribe(data => {
    //     this.userDto = data.result;
    //     this.isModifiedUserOwner = this.userDto.roleId == RoleOption.Owner;

    //     this.repairOrderForm.patchValue({
    //       firstName: this.userDto.firstName,
    //       lastName: this.userDto.lastName,
    //       email: this.userDto.email,
    //       username: this.userDto.username,
    //       phoneNumber: this.userDto.phoneNumber,
    //       repairShopId: this.repairShopUser.repairShopId,
    //       roleId: this.userDto.roleId
    //     });
    //   });


  }


  onYesClick() {
    this.isSubmitted = true;
    if (this.repairOrderForm.valid) {

      const carRepair: CarRepairRequest = {
        carRepairId: this.passedData?.id,
        clientCarId: this.repairOrderForm.get('clientCarId')?.value as number
      };

      this.carRepairService.editCarRepair(carRepair).subscribe(data => {
        if (data.isOk) {
          this.dialogRef.close({
            carRepairId: data.result.carRepairId,
            firstName: data.result.firstName,
            lastName: data.result.lastName,
            licensePlateNumber: data.result.licensePlateNumber,
            statusId: data.result.statusId,
            status: data.result.status,
            repairDateTime: data.result.repairDateTime
          });
        }
        else {
          this.errorMessage = data.message;
        }
      });
    }
  }

  onNoClick() {
    this.dialogRef.close(false);
  }

  onClientChange() {
    const clientId = this.repairOrderForm.get('clientId')?.value as number;
    this.clientCarService.getClientCars(clientId).subscribe(x => {
      this.clientCars = x.result;
    });
  }
}
