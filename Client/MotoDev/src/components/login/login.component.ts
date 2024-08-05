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
        username: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      });
  }


  onSubmit() {
    this.isSubmitted = true;
    if (this.loginForm?.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        response => {
          if (response.isOk){
            localStorage.setItem('authToken', response.message)
          }     
          this.router.navigate(['/mainScreen']);
        },
        error => {
        }
      )
    } 
    else {
    }
  }
  
  onLogin() {
  
  }

  goToRegisterPage() {
    console.log('reg page');
    this.router.navigate(['/register']);
  }
}

