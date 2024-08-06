import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})

export class RoleGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {

    const expectedRole = route.data['expectedRole'];
    if (this.authService.isLoggedIn() && this.authService.hasRole(expectedRole)) {
      return true;
    }
    else {
      localStorage.removeItem('authToken');
      this.router.navigate(['/login']);
      return false;
    }

  }
}

export const roleGuard: CanActivateFn = (route, state) => {
  return true;
};
