import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { SpinnerComponent } from "../spinner/spinner.component";
import { SidebarComponent } from "../sidebar/sidebar.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, LoginComponent, RouterModule, SpinnerComponent, SidebarComponent]
})
export class AppComponent {
 
   title = 'MotoDev';
   
}
