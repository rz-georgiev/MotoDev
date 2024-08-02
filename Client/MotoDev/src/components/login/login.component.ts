import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth/auth.service';
import { Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app/app.routes';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  
  loginForm!: FormGroup; 
  isSubmitted: boolean = false;
  
  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router ) {
      this.loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      });
  }

  get f() { return this.loginForm.controls; }


  onSubmit() {
    this.isSubmitted = true;
    if (this.loginForm?.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        response => {
          console.log('Is good');
          localStorage.setItem('token', response.token)
          this.router.navigate(['/']);
        },
        error => {
          console.log('Is not good');
        }
      )
    } 
    else {
      console.log('Is not good 2');
    }
  }
  
  onLogin() {
  
  }

  goToRegisterPage() {
    console.log('reg page');
    this.router.navigate(['/register']);
  }
}

