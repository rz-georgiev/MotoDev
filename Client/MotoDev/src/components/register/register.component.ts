import { Component } from '@angular/core';
import { EmailValidator, FormBuilder, FormGroup, ReactiveFormsModule, RequiredValidator, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth/auth.service';
import { Router, RouterModule } from '@angular/router';
import { AppRoutingModule, routes } from '../app/app.routes';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule,
     ReactiveFormsModule,
     CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  
  registerForm!: FormGroup;
  isSubmitted: boolean = false;
  errorMessage: string = "";

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router) {
 }
 
  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      acceptTerms: [false, Validators.required]
    });
  }
 

  onSubmit() {
    this.isSubmitted = true;
    if (this.registerForm?.valid) {
      this.authService.register(this.registerForm.value).subscribe(
        response => {      
          if (response.isOk) {
            this.router.navigate(['/confirmAccount']);
          }
          else {
            this.errorMessage = response.message;
          }         
        },
        error => {
          console.error('Registration error', error);
        }
      );
    }
  }
}
