import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { UtcToLocalPipe } from '../../../../core/pipes/utc-to-local.pipe';
import { UserEditorComponent } from '../../../users/components/user-editor/user-editor.component';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationModalComponent } from '../../../../shared/components/confirmation-modal/confirmation-modal.component';
import { RepairOrdersEditorComponent } from '../../../client-car-repairs/components/client-car-repairs-editor/repair-orders-editor.component';
import { CarRepairService } from '../../../client-car-repairs/services/car.repair.service';
import { ClientCarService } from '../../services/client-car.service';
import { ClientCarEditorComponent } from '../client-car-editor/client-car-editor.component';

@Component({
  selector: 'app-client-car',
  standalone: true,
  imports: [CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    UserEditorComponent,
    UtcToLocalPipe],
  templateUrl: './client-car.component.html',
  styleUrl: './client-car.component.css'
})
export class ClientCarComponent {
  displayedColumns: string[] = ['clientName', 'carName', 'licensePlateNumber', 'actions'];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private clientCarService: ClientCarService,
    private matDialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.clientCarService.getAllClientCars().subscribe(data => {
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
    const dialog = this.matDialog.open(ClientCarEditorComponent, {});
    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.dataSource.data.push(result);
        this.dataSource.data = [...this.dataSource.data];
      }
    });
  }

  editAction(element: any): void {
    const dialog = this.matDialog.open(ClientCarEditorComponent, { data: element });
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
      data: { message: "Are you sure you want to deactivate the record?" }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.clientCarService.deactivateByClientCarId(element.clientCarId).subscribe(result => {
          if (result.isOk) {
            this.dataSource.data = this.dataSource.data.filter(x => x.clientCarId !== element.clientCarId);
          }
        });
      }
    });
  }
}
