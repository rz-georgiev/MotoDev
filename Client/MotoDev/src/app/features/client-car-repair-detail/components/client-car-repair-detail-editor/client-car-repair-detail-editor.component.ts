import { CommonModule, DecimalPipe } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { SelectFilterComponent } from '../../../../shared/components/select-filter/select-filter.component';
import { CarResponse } from '../../../cars/models/carResponse';
import { CarService } from '../../../cars/services/car.service';
import { RepairOrdersEditorComponent } from '../../../client-car-repairs/components/client-car-repairs-editor/repair-orders-editor.component';
import { ClientResponse } from '../../../client-car-repairs/models/clientResponse';
import { CarRepairService } from '../../../client-car-repairs/services/car.repair.service';
import { ClientCarEditDto } from '../../../client-cars/models/clientCarEditDto';
import { ClientCarService } from '../../../client-cars/services/client-car.service';
import { ClientService } from '../../../clients/services/client.service';
import { RepairTypeResponse } from '../../../repairTypes/models/repairTypeResponse';
import { RepairStatusResponse } from '../../../repairStatuses/models/repairStatusResponse';
import { RepairTypeService } from '../../../repairTypes/services/repair-type.service';
import { RepairStatusService } from '../../../repairStatuses/services/repair-status.service';
import { CarRepairSelectResponse } from '../../../client-car-repairs/models/carRepairSelectResponse';
import { CarRepairDetailService } from '../../services/car-repair-detail.service';

@Component({
  selector: 'app-client-car-repair-detail-editor',
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
  templateUrl: './client-car-repair-detail-editor.component.html',
  styleUrl: './client-car-repair-detail-editor.component.css'
})
export class ClientCarRepairDetailEditorComponent {
  public repairOrderForm!: FormGroup;
  public clients!: ClientResponse[];
  public cars!: CarResponse[];
  public carsRepairs!: CarRepairSelectResponse[];
  public repairTypes!: RepairTypeResponse[];
  public repairStatuses!: RepairStatusResponse[];
  public isSubmitted!: boolean;
  public isInEditMode: boolean = false;
  public errorMessage!: string;

  constructor(public dialogRef: MatDialogRef<RepairOrdersEditorComponent>,
    @Inject(MAT_DIALOG_DATA) private passedData: any,
    private clientCarService: ClientCarService,
    private repairTypeService: RepairTypeService,
    private repairStatusService: RepairStatusService,
    private carRepairService: CarRepairService,
    private carRepairDetailService: CarRepairDetailService,
    private formBuilder: FormBuilder
  ) {
    this.repairOrderForm = this.formBuilder.group({
      clientCarRepairId: ['', Validators.required],
      repairTypeId: ['', Validators.required],
      statusId: ['', Validators.required],
      price: ['', Validators.required],
    });
  }


  ngOnInit() {
    this.carRepairService.getClientsRepairs().subscribe(data => {
      this.carsRepairs = data.result;
    });

    this.repairTypeService.getAll().subscribe(data => {
      this.repairTypes = data.result;
    });

    this.repairStatusService.getAll().subscribe(data => {
      this.repairStatuses = data.result;
    });


    if (this.passedData?.clientCarRepairDetailId > 0) {
      this.carRepairDetailService.getById(this.passedData.clientCarRepairDetailId).subscribe(x => {
        this.repairOrderForm.patchValue({
          clientCarRepairId: x.result.clientCarRepairId,
          repairTypeId: x.result.repairTypeId,
          statusId: x.result.repairStatusId,
          price: x.result.price.toFixed(2),
        }); 
      });
    }
  }


  onYesClick() {
    this.isSubmitted = true;
    if (this.repairOrderForm.valid) {

      const clientCarDto: ClientCarEditDto = {
        clientCarId: this.passedData?.clientCarRepairDetailId,
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
