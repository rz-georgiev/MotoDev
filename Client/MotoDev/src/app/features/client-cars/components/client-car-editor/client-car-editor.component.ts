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
import { SelectFilterComponent } from '../../../../shared/components/select-filter/select-filter.component';
import { RepairOrdersEditorComponent } from '../../../client-car-repairs/components/client-car-repairs-editor/repair-orders-editor.component';
import { CarRepairRequest } from '../../../client-car-repairs/models/carRepairRequest';
import { ClientCarResponse } from '../../../client-car-repairs/models/clientCarResponse';
import { ClientResponse } from '../../../client-car-repairs/models/clientResponse';
import { CarRepairService } from '../../../client-car-repairs/services/car.repair.service';
import { ClientService } from '../../../clients/services/client.service';
import { ClientCarService } from '../../services/client-car.service';
import { CarResponse } from '../../../cars/models/carResponse';
import { CarService } from '../../../cars/services/car.service';
import { ClientCarEditDto } from '../../models/clientCarEditDto';

@Component({
  selector: 'app-client-car-editor',
  standalone: true,
  imports: [CommonModule,
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
    SelectFilterComponent],
  templateUrl: './client-car-editor.component.html',
  styleUrl: './client-car-editor.component.css'
})
export class ClientCarEditorComponent {
  public repairOrderForm!: FormGroup;
  public clients!: ClientResponse[];
  public cars!: CarResponse[];
  public isSubmitted!: boolean;
  public isInEditMode: boolean = false;
  public errorMessage!: string;

  constructor(public dialogRef: MatDialogRef<RepairOrdersEditorComponent>,
    @Inject(MAT_DIALOG_DATA) private passedData: any,
    private carRepairService: CarRepairService,
    private clientService: ClientService,
    private clientCarService: ClientCarService,
    private carService: CarService,
    private formBuilder: FormBuilder
  ) {
    this.repairOrderForm = this.formBuilder.group({
      clientId: ['', Validators.required],
      carId: ['', Validators.required],
      licensePlateNumber: ['', Validators.required]
    });
  }


  ngOnInit() {

    this.isInEditMode = this.passedData?.clientCarId > 0;

    this.clientService.getClients().subscribe(x => {
      this.clients = x.result;
    });

    this.carService.getAllCars().subscribe(data => {
      this.cars = data.result;
    });

    if (this.passedData?.clientCarId > 0) {
      this.clientCarService.getById(this.passedData.clientCarId).subscribe(x => {
        this.repairOrderForm.patchValue({
          clientId: x.result.clientId,
          carId: x.result.carId,
          licensePlateNumber: x.result.licensePlateNumber
        }); 
      });
    }
  }


  onYesClick() {
    this.isSubmitted = true;
    if (this.repairOrderForm.valid) {

      const clientCarDto: ClientCarEditDto = {
        clientCarId: this.passedData?.clientCarId,
        clientId: this.repairOrderForm.value.clientId,
        carId: this.repairOrderForm.value.carId,
        licensePlateNumber: this.repairOrderForm.value.licensePlateNumber,
      };

      this.clientCarService.edit(clientCarDto).subscribe(data => {
        if (data.isOk) {
          this.dialogRef.close({
            clientCarId: data.result.clientCarId,
            clientName: data.result.clientName,
            carName: data.result.carName,
            licensePlateNumber: data.result.licensePlateNumber,
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
}
