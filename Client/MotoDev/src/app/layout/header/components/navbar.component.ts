import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ConfirmAccountComponent } from "../../../features/auth/components/confirm-account/confirm-account.component";
import { NavbarService } from '../services/navbar.service';
import { UsersComponent } from '../../../features/users/components/users/users.component';
import { AuthService } from '../../../features/auth/services/auth.service';
import { ConfirmationModalComponent } from '../../../shared/components/confirmation-modal/confirmation-modal.component';
import { MatDialog, matDialogAnimations } from '@angular/material/dialog';
import { CurrentUser } from '../../../features/auth/models/currentUser';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmAccountComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {


  currentUserFullName: string | undefined;
  isSidebarOpened: boolean = true;
  currentUserShowName: string | undefined;
  currentUserRole: string | undefined;
  currentUser!: CurrentUser;
  
  constructor(private router: Router,
    private authService: AuthService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.currentUser = this.authService.currentUser;
    this.currentUserShowName = `${this.currentUser.firstName.charAt(0)}. ${this.currentUser.lastName}`;
    this.currentUserFullName = `${this.currentUser.firstName} ${this.currentUser.lastName}`;
    this.currentUserRole = this.currentUser.role;
  }

  customizeProfile() {
    // this.router.navigate(['/userProfile'], {queryParams: {id: 1}});
  }

  handleConfirmation() {
    this.dialog.open(ConfirmationModalComponent, {data: {message: "Would you like to sign out?"}}).afterClosed().subscribe(result => {
      if (result) {
        this.authService.signOut();
      }     
    });
  }
  
  toggleSidebar() {
    this.isSidebarOpened = !this.isSidebarOpened;
    if (this.isSidebarOpened) {
      document.body.classList.remove('toggle-sidebar');
    }
    else {
      document.body.classList.add('toggle-sidebar');
    }
  }
}
