import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../features/auth/services/auth.service';


@Injectable({
  providedIn: 'root'
})

export class RoleGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {

    const roles = route.data['roles'];
    if (this.authService.isLoggedIn() && this.authService.hasAnyOfTheRoles(roles)) {
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
