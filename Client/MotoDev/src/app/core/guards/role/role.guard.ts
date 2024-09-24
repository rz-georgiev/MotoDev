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
      const userRoles = this.authService.getUserRoles();
      if (userRoles.includes('Client')) {
        this.router.navigate(['/repairTracker']);
      }
      else if (userRoles.includes('Mechanic')) {
        this.router.navigate(['/mechanicRepairs']);
      }
      else {
        // this.authService.signOut();
        this.router.navigate(['/app-not-found']);
        return true;
      } 
      return false;
    }
  }
}

export const roleGuard: CanActivateFn = (route, state) => {
  return true;
};
