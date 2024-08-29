import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { SpinnerService } from '../../../shared/services/spinner.service';
import { Observable, finalize } from 'rxjs';

export const spinnerInterceptor: HttpInterceptorFn = (req, next) => {

  const spinnerService = inject(SpinnerService);
  spinnerService.show();

  
  return next(req).pipe();
};

@Injectable()
export class SpinnerInterceptor implements HttpInterceptor {
  constructor(private spinnerService: SpinnerService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.spinnerService.show();
    return next.handle(req).pipe(
      finalize(() => this.spinnerService.hide())
    );
  }
}

