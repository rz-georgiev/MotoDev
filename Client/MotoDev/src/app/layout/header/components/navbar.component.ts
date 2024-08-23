import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ConfirmAccountComponent } from "../../../features/auth/components/confirm-account/confirm-account.component";
import { ConfirmationModalComponent } from "../../../features/auth/components/confirmation-modal/confirmation-modal.component";
import { NavbarService } from '../services/navbar.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmAccountComponent, ConfirmationModalComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  currentUsername: string | undefined;
  
  constructor(private router: Router, private navbarService: NavbarService) { }

  handleConfirmation(isConfirmed: boolean) {
    if (isConfirmed) {
      localStorage.removeItem('authToken');
      this.router.navigate(['/login']);
    }
  }

  toggleSidebar() {
    this.navbarService.toggleSidebar();
  }
}
