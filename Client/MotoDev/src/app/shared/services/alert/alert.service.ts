import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  private alertSubject = new Subject<string>();
  private alertTimer: any;

  constructor() {}

  getAlert(): Observable<string> {
    return this.alertSubject.asObservable();
  }

  showAlert(message: string, duration: number = 3000): void {
    this.alertSubject.next(message);

    if (this.alertTimer) {
      clearTimeout(this.alertTimer);
    }

    this.alertTimer = setTimeout(() => {
      this.alertSubject.next('ghjghj');
    }, duration);
  }
}
