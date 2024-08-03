import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { AuthComponent } from '../auth/auth.component';
import { ForgottenPasswordComponent } from '../forgotten-password/forgotten-password.component';
import { CommonModule } from '@angular/common';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'forgottenPassword', component: ForgottenPasswordComponent },
    { path: '',  component: AuthComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes), 
        ReactiveFormsModule,
        CommonModule],
    exports: [RouterModule],
})

export class AppRoutingModule { }

