import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AppRoutingModule } from '../../../app.routes';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../features/auth/services/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  
  public openedTabName: string = "Dashboard";

  constructor(public authService: AuthService) { }

  public toggleTab(tabName: string) {
    this.openedTabName = tabName;
  }
}
