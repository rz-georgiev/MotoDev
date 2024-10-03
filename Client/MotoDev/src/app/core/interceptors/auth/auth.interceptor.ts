import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, finalize, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../../../features/auth/services/auth.service';
import { AlertService } from '../../../shared/services/alert/alert.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);
  const authService = inject(AuthService);
  const alertService = inject(AlertService);
  const authToken = localStorage.getItem('authToken');
  const clonedRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${authToken}`
    }
  });
  
  return next(clonedRequest).pipe(
    catchError((err: any) => {
      if (err instanceof HttpErrorResponse) {
        // Handle HTTP errors
        if (err.status === 401) {
          authService.signOut();
        } else {
          // Handle other HTTP error codes
          console.error('HTTP error:', err);
          alertService.showAlert(err.message, 3000);
        }
      } else {
        // Handle non-HTTP errors
        console.error('An error occurred:', err);
        alertService.showAlert(err.message, 3000);
      }

      // Re-throw the error to propagate it further
      return throwError(() => err); 
    })
  );;
};