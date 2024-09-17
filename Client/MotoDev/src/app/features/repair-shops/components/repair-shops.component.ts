import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { MatFormField, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTable, MatTableDataSource, MatTableModule } from '@angular/material/table';
import { UserEditorComponent } from '../../users/components/user-editor/user-editor.component';
import { RepairShopDto } from '../models/repairShopDto';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { RepairShopService } from '../services/repair-shop.service';
import { AuthService } from '../../auth/services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationModalComponent } from '../../../shared/components/confirmation-modal/confirmation-modal.component';
import { config } from 'rxjs';

@Component({
  selector: 'app-repair-shops',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    UserEditorComponent],
  templateUrl: './repair-shops.component.html',
  styleUrl: './repair-shops.component.css'
})
export class RepairShopsComponent {

  displayedColumns: string[] = ['name', 'address', 'vatNumber', 'actions'];
  dataSource = new MatTableDataSource<RepairShopDto>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private repairShopService: RepairShopService,
    private authService: AuthService,
    private matDialog: MatDialog
  ) { }

  ngOnInit() {
    this.repairShopService.getRepairShopsForSpecifiedOwner(this.authService.currentUser.id).subscribe(data => {
      this.dataSource.data = data.result;
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter($event: KeyboardEvent) {
    const filterValue = ($event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  addAction() {
    throw new Error('Method not implemented.');
  }

  editAction(element: any) {
    throw new Error('Method not implemented.');
  }

  deleteAction(element: any) {
    this.matDialog.open(ConfirmationModalComponent, { data: { message: 'Are you sure you want to deactivate the record?' } })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.repairShopService.deactivateById(element.id).subscribe(data => {
            this.dataSource.data = this.dataSource.data.filter(x => x.id !== element.id);
          });
        }
      });
  }
}
