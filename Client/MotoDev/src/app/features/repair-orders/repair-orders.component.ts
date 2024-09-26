import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { ConfirmationModalComponent } from '../../shared/components/confirmation-modal/confirmation-modal.component';
import { UserEditorComponent } from '../users/components/user-editor/user-editor.component';
import { UserService } from '../users/services/user.service';

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
    UserEditorComponent],
  templateUrl: './repair-orders.component.html',
  styleUrl: './repair-orders.component.css'
})
export class RepairOrdersComponent {
  displayedColumns: string[] = ['firstName', 'lastName', 'licensePlateNumber', 'status', 'actions'];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private userService: UserService,
    private matDialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.userService.getAllForCurrentOwnerUserId().subscribe(data => {
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
    const dialog = this.matDialog.open(UserEditorComponent, { });
    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.dataSource.data.push(result);       
        this.dataSource.data = [...this.dataSource.data];
      }
    });

  }

  editAction(element: any): void {
    const dialog = this.matDialog.open(UserEditorComponent, {data: element});
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
        this.userService.deactivateRepairUserById(element.repairShopUserId).subscribe(result => {
          if (result.isOk) {
            this.dataSource.data = this.dataSource.data.filter(x => x.repairShopUserId !== element.repairShopUserId);
          }
        });
      }
    });
  }
}
