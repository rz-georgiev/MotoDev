import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { ConfirmationModalComponent } from '../../../../shared/components/confirmation-modal/confirmation-modal.component';
import { UserEditorComponent } from '../../../users/components/user-editor/user-editor.component';
import { UserService } from '../../../users/services/user.service';
import { CarRepairService } from '../../services/car.repair.service';
import { UtcToLocalPipe } from "../../../../core/pipes/utc-to-local.pipe";
import { RepairOrdersEditorComponent } from '../client-car-repairs-editor/repair-orders-editor.component';

@Component({
  selector: 'app-repair-orders',
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
    UtcToLocalPipe
],
  templateUrl: './repair-orders.component.html',
  styleUrl: './repair-orders.component.css'
})
export class RepairOrdersComponent {
  displayedColumns: string[] = ['carRepairId', 'firstName', 'lastName', 'licensePlateNumber', 'status', 'repairDateTime', 'actions'];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private carRepairService: CarRepairService,
    private matDialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.carRepairService.getAllCarsRepairs().subscribe(data => {
      this.dataSource.data = data.result;
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  addAction(): void {
    const dialog = this.matDialog.open(RepairOrdersEditorComponent, { });
    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.dataSource.data.push(result);       
        this.dataSource.data = [...this.dataSource.data];
      }
    });
  }

  editAction(element: any): void {
    const dialog = this.matDialog.open(RepairOrdersEditorComponent, {data: element});
    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.dataSource.data = this.dataSource.data.filter(x => x !== element);
        this.dataSource.data.push(result);
        this.dataSource.data = [...this.dataSource.data];
      }
    });
  }

  deleteAction(element: any): void {
    let dialogRef = this.matDialog.open(ConfirmationModalComponent, {
      data: { message: "Are you sure you want to deactivate the repair and its details?" }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.carRepairService.deactivateByCarRepairIdAsync(element.carRepairId).subscribe(result => {
          if (result.isOk) {
            this.dataSource.data = this.dataSource.data.filter(x => x.carRepairId !== element.carRepairId);
          }
        });
      }
    });
  }
}
