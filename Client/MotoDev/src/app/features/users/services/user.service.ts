import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { ExtendedJwtPayload } from '../../auth/models/extendedJwtPayload';
import { UserResponse } from '../models/userResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  
  private baseUrl = 'https://localhost:5078/Account';
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();
  
  constructor(private httpClient: HttpClient) {}

  public login(user: any): Observable<UserResponse> {
    return this.httpClient.post<UserResponse>(`${this.baseUrl}/Login`, user);
  
  }
}
