import { CommonModule, ɵparseCookieValue } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../../../../app.routes';
import { SpinnerService } from '../../../../shared/services/spinner.service';
import { delay } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule], templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {

  loginForm!: FormGroup;
  isSubmitted: boolean = false;
  errorMessage: string = "";

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
  private spinner: SpinnerService) {

    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
      // rememberMe: ['', Validators.required]
    });
  }


  onSubmit() {
    this.isSubmitted = true;
    this.spinner.show();
    if (this.loginForm?.valid) {   
      this.authService.login(this.loginForm.value).subscribe(
        response => {
          if (response.isOk) {
            localStorage.setItem('authToken', response.message)
            this.router.navigate(['/mainScreen']); 
          }
          else {
            this.errorMessage = response.message;
          }  
          this.spinner.hide();      
        },
        error => {
          this.spinner.hide();
        }
      )
    }
    else {
      this.spinner.hide();
    }
  }
  
  goToRegisterPage() {
    console.log('reg page');
    this.router.navigate(['/register']);
  }
}

