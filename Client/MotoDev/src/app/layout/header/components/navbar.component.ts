import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ConfirmAccountComponent } from "../../../features/auth/components/confirm-account/confirm-account.component";
import { NavbarService } from '../services/navbar.service';
import { UsersComponent } from '../../../features/users/components/users/users.component';
import { AuthService } from '../../../features/auth/services/auth.service';

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
  
  constructor(private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
    const currentUser = this.authService.currentUser;
    this.currentUserShowName = `${currentUser.firstName.charAt(0)}. ${currentUser.lastName}`;
    this.currentUserFullName = `${currentUser.firstName} ${currentUser.lastName}`;
    this.currentUserRole = currentUser.role;
  }

  handleConfirmation(isConfirmed: boolean) {
    if (isConfirmed) {
      localStorage.removeItem('authToken');
      this.router.navigate(['/login']);
    }
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
