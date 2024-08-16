import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { AuthComponent } from '../auth/auth.component';
import { ForgottenPasswordComponent } from '../forgotten-password/forgotten-password.component';
import { CommonModule } from '@angular/common';
import { MainScreenComponent } from '../main-screen/main-screen.component';
import { RoleGuard } from '../../guards/role/role.guard';
import { ConfirmAccountComponent } from '../confirm-account/confirm-account.component';
import { ForgottenPasswordConfirmComponent } from '../forgotten-password-confirm/forgotten-password-confirm.component';
import { UsersComponent } from '../users/users.component';
import { ClientsComponent } from '../clients/clients.component';
import { RepairsComponent } from '../repairs/repairs.component';
import { StatisticsComponent } from '../statistics/statistics.component';
import { CarsComponent } from '../cars/cars.component';
import { RepairShopsComponent } from '../repair-shops/repair-shops.component';
import { BaseDataComponent } from '../base-data/base-data.component';
import { AboutComponent } from '../about/about.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'forgottenPassword', component: ForgottenPasswordComponent },
    { path: 'forgottenPasswordConfirm/:resetPasswordHash', component: ForgottenPasswordConfirmComponent },
    {
        path: 'mainScreen',
        component: MainScreenComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrator', 'Owner', 'Mechanic'] }
    },
    { path: 'confirmAccount/:accountConfirmationHash', component: ConfirmAccountComponent },
    { path: 'confirmAccount', component: ConfirmAccountComponent },
    { path: 'users', component: UsersComponent },
    { path: 'clients', component: ClientsComponent },
    { path: 'repairs', component: RepairsComponent },
    { path: 'statistics', component: StatisticsComponent },
    { path: 'cars', component: CarsComponent },
    { path: 'repairShops', component: RepairShopsComponent },
    { path: 'baseData', component: BaseDataComponent },
    { path: 'about', component: AboutComponent },
    { path: '', component: AuthComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes),
        ReactiveFormsModule,
        CommonModule],
    exports: [RouterModule],
})

export class AppRoutingModule { }

