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

  isSideBarOpened: boolean = true;

  constructor(private navbarService: NavbarService, private renderer: Renderer2) { }

  ngAfterViewInit() {
    this.navbarService.isSideBarOpened$.subscribe(result => {
      this.isSideBarOpened = result;
      if (result) {
        document.body.classList.add('toggle-sidebar');
      }
      else {
        document.body.classList.remove('toggle-sidebar');
      }
    });
  }

}
