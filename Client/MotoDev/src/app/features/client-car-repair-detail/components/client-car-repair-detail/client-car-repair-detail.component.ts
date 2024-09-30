import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogActions, MatDialogClose, MatDialogContent } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CarRepairDetailService } from '../../services/car-repair-detail.service';
import { RepairOrdersEditorComponent } from '../../../client-car-repairs/components/client-car-repairs-editor/repair-orders-editor.component';
import { ClientCarEditorComponent } from '../../../client-cars/components/client-car-editor/client-car-editor.component';
import { UserEditorComponent } from '../../../users/components/user-editor/user-editor.component';
import { ConfirmationModalComponent } from '../../../../shared/components/confirmation-modal/confirmation-modal.component';
import { ClientCarRepairDetailEditorComponent } from '../client-car-repair-detail-editor/client-car-repair-detail-editor.component';

@Component({
  selector: 'app-client-car-repair-detail',
  standalone: true,
  imports: [    CommonModule,
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
    MatButtonModule],
  templateUrl: './client-car-repair-detail.component.html',
  styleUrl: './client-car-repair-detail.component.css'
})
export class ClientCarRepairDetailComponent {

  constructor(private carRepairDetailService: CarRepairDetailService,
    private matDialog: MatDialog
  ) { }

  displayedColumns: string[] = ['clientName', 'licensePlateNumber', 'repairTypeName', 'price', 'status', 'actions'];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  ngOnInit(): void {
    this.carRepairDetailService.getAll().subscribe(data => {
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
    const dialog = this.matDialog.open(ClientCarRepairDetailEditorComponent, {});
    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.dataSource.data.push(result);
        this.dataSource.data = [...this.dataSource.data];
      }
    });
  }

  editAction(element: any): void {
    const dialog = this.matDialog.open(ClientCarRepairDetailEditorComponent, { data: element });
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

    // dialogRef.afterClosed().subscribe(result => {
    //   if (result === true) {
    //     this.clientCarService.deactivateByClientCarId(element.clientCarId).subscribe(result => {
    //       if (result.isOk) {
    //         this.dataSource.data = this.dataSource.data.filter(x => x.clientCarId !== element.clientCarId);
    //       }
    //     });
    //   }
    // });
  }
}
