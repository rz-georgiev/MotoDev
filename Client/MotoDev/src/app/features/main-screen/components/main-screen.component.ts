import { CommonModule } from '@angular/common';
import { Component, Renderer2 } from '@angular/core';
import { NavbarService } from '../../../layout/header/services/navbar.service';
import { RedirectCommand } from '@angular/router';

@Component({
  selector: 'app-main-screen',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './main-screen.component.html',
  styleUrl: './main-screen.component.css'
})
export class MainScreenComponent {

  constructor(private navbarService: NavbarService) { }


}
