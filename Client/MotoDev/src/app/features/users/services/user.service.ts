import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { ExtendedJwtPayload } from '../../auth/models/extendedJwtPayload';
import { UserResponse } from '../models/userResponse';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = `https://localhost:5078`; // Example API

  constructor(private http: HttpClient,
    private authService: AuthService) { }

  getData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Users/GetAllForCurrentOwnerUserId?ownerUserId=${this.authService.currentUserId}`);
  }
}
