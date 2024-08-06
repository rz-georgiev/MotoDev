import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-confirm-account',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, CommonModule],
  templateUrl: './confirm-account.component.html',
  styleUrl: './confirm-account.component.css'
})
export class ConfirmAccountComponent {
forgottenPasswordForm: any;
isSubmitted: any;
onSubmit() {
throw new Error('Method not implemented.');
}

}
