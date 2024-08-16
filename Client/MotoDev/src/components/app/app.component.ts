import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { SpinnerComponent } from "../spinner/spinner.component";
import { SidebarComponent } from "../sidebar/sidebar.component";
import { NavbarComponent } from "../navbar/navbar.component";
import { AuthService } from '../../services/auth/auth.service';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, LoginComponent, RouterModule, SpinnerComponent, SidebarComponent, NavbarComponent]
})
export class AppComponent {
 
   title = 'MotoDev';
   isLoggedIn: boolean = false;

   constructor(private authService: AuthService) { }

   ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
   }
}
