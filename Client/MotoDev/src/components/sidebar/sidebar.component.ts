import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AppRoutingModule } from '../app/app.routes';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  isToggled: boolean = false;
  toggleStuff() {
    this.isToggled = !this.isToggled;
  }
}
