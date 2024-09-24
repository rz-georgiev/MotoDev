import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/auth/components/login/login.component';
import { RegisterComponent } from './features/auth/components/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { AuthComponent } from './features/auth/components/main/auth.component';
import { ForgottenPasswordComponent } from './features/auth/components/forgotten-password/forgotten-password.component';
import { CommonModule } from '@angular/common';
import { MainScreenComponent } from './features/main-screen/components/main-screen.component';
import { ConfirmAccountComponent } from './features/auth/components/confirm-account/confirm-account.component';
import { ForgottenPasswordConfirmComponent } from './features/auth/components/forgotten-password-confirm/forgotten-password-confirm.component';
import { UsersComponent } from './features/users/components/users/users.component';
import { ClientsComponent } from './features/clients/components/clients.component';
import { RepairsComponent } from './features/repairs/components/repairs.component';
import { StatisticsComponent } from './features/statistics/components/statistics.component';
import { CarsComponent } from './features/cars/components/cars.component';
import { RepairShopsComponent } from './features/repair-shops/components/repair-shops/repair-shops.component';
import { BaseDataComponent } from './features/baseData/components/base-data.component';
import { AboutComponent } from './features/about/components/about.component';
import { RoleGuard } from './core/guards/role/role.guard';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { animation } from '@angular/animations';
import { UserProfileComponent } from './features/user-profile/user-profile.component';
import { RepairTrackerComponent } from './repair-tracker/components/repair-tracker.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'forgottenPassword', component: ForgottenPasswordComponent },
    { path: 'forgottenPasswordConfirm/:resetPasswordHash', component: ForgottenPasswordConfirmComponent },
    {
        path: 'mainScreen',
        component: MainScreenComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Owner'] }
    },
    { path: 'confirmAccount/:accountConfirmationHash', component: ConfirmAccountComponent },
    { path: 'confirmAccount', component: ConfirmAccountComponent },
    {
        path: 'users',
        component: UsersComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Owner'] }
    },
    // { path: 'clients', component: ClientsComponent,  canActivate: [RoleGuard],  data: { roles: ['Owner'] } },
    {
        path: 'repairs',
        component: RepairsComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Mechanic'] }
    },
    // { path: 'statistics', component: StatisticsComponent },
    {
        path: 'repairTracker',
        component: RepairTrackerComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Client'] }
    },
    {
        path: 'cars',
        component: CarsComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Owner'] }
    },
    {
        path: 'repairShops',
        component: RepairShopsComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Owner'] }
    },
    {
        path: 'baseData',
        component: BaseDataComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Owner'] }
    },
    {
        path: 'about',
        component: AboutComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Owner', 'Client', 'Mechanic'] }
    },
    {
        path: 'userProfile',
        component: UserProfileComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Owner', 'Client', 'Mechanic'] }
    },
    { path: '', component: AuthComponent },
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes),
        ReactiveFormsModule,
        BrowserModule,
        CommonModule,
    ],
    exports: [RouterModule],
})

export class AppRoutingModule { }

