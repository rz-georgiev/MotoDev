import { Component } from '@angular/core';
import { LoginComponent } from "../login/login.component";
import { AuthService } from '../../services/auth/auth.service';
import { MainScreenComponent } from "../main-screen/main-screen.component";
import { Router, RouterModule } from '@angular/router';
import { SidebarComponent } from "../sidebar/sidebar.component";
import { NavbarComponent } from "../navbar/navbar.component";

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [LoginComponent, MainScreenComponent, RouterModule, SidebarComponent, NavbarComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  isLoggedIn: boolean = false;
  
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
    // if (this.isLoggedIn) {
    //   this.router.navigate(['/mainScreen']);
    // }
    // else {
    //   this.router.navigate(['/login']);
    // }
  }
}
