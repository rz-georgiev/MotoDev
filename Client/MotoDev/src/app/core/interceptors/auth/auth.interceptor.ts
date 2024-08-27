import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, throwError } from 'rxjs';
import { AuthService } from '../../../features/auth/services/auth.service';
import { Router } from '@angular/router';
import { SpinnerService } from '../../../shared/services/spinner.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService, 
    private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = localStorage.getItem('authToken');
    const authRequest = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${authToken}`)
    });
    return next.handle(authRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          localStorage.removeItem('authToken');
          this.router.navigate(['/login']);
        }
        return throwError(error);
      })
    );
  }
}