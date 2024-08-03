import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgotten-password',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './forgotten-password.component.html',
  styleUrl: './forgotten-password.component.css'
})
export class ForgottenPasswordComponent {
 
  isSubmitted: boolean = false;
  forgottenPasswordForm!: FormGroup;

  constructor(private authService: AuthService, 
    private router: Router,
     private httpClient: HttpClient,
    private formBuilder: FormBuilder) {

  }

  ngOnInit() {
    this.forgottenPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }
  
  onSubmit() {
    this.isSubmitted = true;
    if (this.forgottenPasswordForm.valid) {
      this.authService.forgotPassword(this.forgottenPasswordForm.value).subscribe(
        response => {

        },
        error => {
          
        }
      )
    }
     else {
      
    }
  }
}
