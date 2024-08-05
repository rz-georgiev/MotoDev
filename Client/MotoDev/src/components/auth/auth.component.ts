import { Component } from '@angular/core';
import { LoginComponent } from "../login/login.component";
import { AuthService } from '../../services/auth/auth.service';
import { MainScreenComponent } from "../main-screen/main-screen.component";

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [LoginComponent, MainScreenComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  isLoggedIn: boolean = false;
  
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
  }
}
