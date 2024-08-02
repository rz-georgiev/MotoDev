import { Component } from '@angular/core';
import { FormBuilder, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth/auth.service';
import { Router, RouterModule } from '@angular/router';
import { AppRoutingModule, routes } from '../app/app.routes';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  
  registerForm: FormGroup | undefined;

  constructor(private formBuilder: FormBuilder,
     private authService: AuthService,
     private router: Router) {
      this.registerForm = this.formBuilder.group({
        username: ['', Validators.required],
        email: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(8)]]
      });
  }

  onSubmit() {
    if (this.registerForm?.valid) {
      this.authService.register(this.registerForm.value).subscribe(
        response => {
          console.log('Registration successful', response);
          this.router.navigate(['/login']);
        },
        error => {
          console.error('Registration error', error);
        }
      );
    }
  }
}
