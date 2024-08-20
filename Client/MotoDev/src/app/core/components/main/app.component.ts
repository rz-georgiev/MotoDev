import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { LoginComponent } from '../../../features/auth/components/login/login.component';
import { SpinnerComponent } from "../../../shared/components/spinner/spinner.component";
import { SidebarComponent } from "../../../layout/sidebar/components/sidebar.component";
import { NavbarComponent } from "../../../layout/header/components/navbar.component";
import { AuthService } from '../../../features/auth/services/auth.service';

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
        this.authService.isLoggedIn$.subscribe(result => {
            this.isLoggedIn = result;
        });
    }
}
