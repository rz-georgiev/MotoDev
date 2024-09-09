import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon'
import { UserEditorComponent as UserEditorComponent } from '../user-editor/user-editor.component';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationModalComponent } from '../../../../shared/components/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-users',
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
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {

  displayedColumns: string[] = ['firstName', 'lastName', 'repairShop', 'position', 'actions'];
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
