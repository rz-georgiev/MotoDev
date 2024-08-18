import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-confirm-account',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, CommonModule],
  templateUrl: './confirm-account.component.html',
  styleUrl: './confirm-account.component.css'
})
export class ConfirmAccountComponent {

  private confirmationHash: string | null;
  public resultMessage?: string | null;
  
  constructor(private route: ActivatedRoute, private authService: AuthService) {
    this.confirmationHash = this.route.snapshot.paramMap.get('accountConfirmationHash');
  }

  ngOnInit() {
    if (this.confirmationHash != null) {
      this.authService.confirmAccount({'confirmHash': this.confirmationHash}).subscribe(response => {
       this.resultMessage = response.message;
      }, 
      error => {
      });
      
    }
    else {
      this.resultMessage = "Please check your email for confirmation link";
    }
  }

}
