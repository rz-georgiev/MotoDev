import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgotten-password',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './forgotten-password.component.html',
  styleUrl: './forgotten-password.component.css',
})
export class ForgottenPasswordComponent {

  forgottenPasswordForm!: FormGroup;
  isSubmitted: boolean = false;
  sentEmailMessage: string = '';

  constructor(private authService: AuthService,
    private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.forgottenPasswordForm = this.formBuilder.group({
      recipientEmail: ['', [Validators.required, Validators.email]]
    });

  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.forgottenPasswordForm.valid) {
      this.authService.forgotPassword(this.forgottenPasswordForm.value).subscribe(
        response => {
          this.sentEmailMessage = response.message;
        },
        error => {
          this.sentEmailMessage = "An error occurred while contacting the server";
        }
      )
    }
  }
}
