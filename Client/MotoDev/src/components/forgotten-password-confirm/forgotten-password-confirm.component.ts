import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-forgotten-password-confirm',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './forgotten-password-confirm.component.html',
  styleUrl: './forgotten-password-confirm.component.css'
})
export class ForgottenPasswordConfirmComponent {

  public resultMessage: string | null | undefined;
  public resetPasswordHash: string | null;
  public resetPasswordForm!: FormGroup;
  public isSubmitted: boolean = false;
  public isPasswordChanged: boolean = false;

  constructor(private route: ActivatedRoute,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    this.resetPasswordHash = route.snapshot.paramMap.get('resetPasswordHash'); 
  }

  ngOnInit() {
    this.resetPasswordForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(8)]]
   });
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.resetPasswordForm.valid) {   
      const model = this.resetPasswordForm.value;
      if (model.password != model.confirmPassword){
        this.resultMessage = 'Passwords do no not match, please check them again';
        return;
      }
      this.authService.resetPassword({
        resetPasswordToken: this.resetPasswordHash,
        password: this.resetPasswordForm.value.password
      }).subscribe(response => {
        this.isPasswordChanged = response.isOk;
        this.resultMessage = response.message;
      },
        error => {
          this.resultMessage = 'An error occurred while contacting the server';
        });
    }
  }
}
