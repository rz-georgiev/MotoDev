import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ConfirmAccountComponent } from "../confirm-account/confirm-account.component";
import { ConfirmationModalComponent } from "../confirmation-modal/confirmation-modal.component";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmAccountComponent, ConfirmationModalComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  currentUsername: string | undefined;
  
  constructor(private router: Router) { }

  handleConfirmation(isConfirmed: boolean) {
    if (isConfirmed) {
      localStorage.removeItem('authToken');
      this.router.navigate(['/login']);
    }
    else {
      
    } 
  }
}
