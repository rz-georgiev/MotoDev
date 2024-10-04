import { Component, ElementRef, ViewChild } from '@angular/core';
import { EmailValidator, FormBuilder, FormGroup, ReactiveFormsModule, RequiredValidator, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { AppRoutingModule, routes } from '../../../../app.routes';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationModalComponent } from '../../../../shared/components/confirmation-modal/confirmation-modal.component';
import { InfoModalComponent } from '../../../../shared/components/info-modal/info-modal.component';

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
  gdprText: string = `"GDPR Consent AgreementWe value your privacy and are committed to protecting your personal data. In compliance with the General Data Protection Regulation (GDPR), we request your consent to collect, store, and process your personal information for the purposes outlined below.1. Data We Collect:NameEmail AddressContact InformationAny other information you voluntarily provide2. Purpose of Data Collection:We collect and process your data for the following purposes:Providing the services you requestedSending updates, newsletters, and promotional materialsImproving our products and servicesComplying with legal obligations3. Data Storage and Security:Your personal data will be stored securely and only accessed by authorized personnel. We implement strict data protection measures to safeguard your information from unauthorized access, disclosure, or alteration.4. Your Rights:Under the GDPR, you have the following rights regarding your personal data:The right to access your dataThe right to rectify any inaccuraciesThe right to request the deletion of your dataThe right to withdraw consent at any timeThe right to data portability5. Data Sharing:We will not share your personal data with third parties without your explicit consent unless required by law.Consent:By clicking "I Agree" or signing this form, you confirm that you have read and understood this agreement and give us consent to collect, store, and process your personal data in accordance with the GDPR and the terms outlined above."`

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private matDialog: MatDialog) {
 }
 
  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      acceptTerms: [false, Validators.required]
    });
  }
 
  checkTermsAndConditions() {
    this.matDialog.open(InfoModalComponent, {
      data: { message: this.gdprText}
    });
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.registerForm?.valid) {
      // Temp
      this.registerForm.value.username = this.registerForm.value.email;
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
