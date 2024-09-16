import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';
import { ExtendedJwtPayload } from '../models/extendedJwtPayload';
import { CurrentUser } from '../models/currentUser';
import { Router } from '@angular/router';
import { UserService } from '../../users/services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'https://localhost:5078/Accounts';
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();
  public currentUser!: CurrentUser;

  constructor(private httpClient: HttpClient,
    private router: Router) { }

  signOut() {
    localStorage.removeItem('authToken');
    this.router.navigate(['/login']);
    this.isLoggedInSubject.next(false);
  }

  isLoggedIn(): boolean {
    const authToken = localStorage.getItem('authToken');
    if (!authToken) {
      this.isLoggedInSubject.next(false);
      return false;
    }

    const decoded = this.getDecodedToken(authToken);
    this.currentUser = {
      id: decoded.userId,
      firstName: decoded.unique_name,
      lastName: decoded.family_name,
      username: decoded.nameid,
      role: Array.isArray(decoded.role) ? decoded.role.at(0) : decoded.role,
      imageUrl: decoded.imageUrl
    };

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp ?? 0);

    const hasExpired = date.valueOf() <= Date.now().valueOf();
    const result = !!authToken && !hasExpired;

    this.isLoggedInSubject.next(result);
    return result;
  }

  getUserRoles(): string[] {
    const authToken = localStorage.getItem('authToken');
    if (!authToken) {
      return [];
    }

    try {
      return this.getDecodedToken(authToken).role;
    }
    catch (error) {
      return [];
    }
  }

  getDecodedToken(token: string) {
    const decoded = jwtDecode<ExtendedJwtPayload>(token);
    return decoded;
  }

  hasAnyOfTheRoles(roles: string[]): boolean {
    const userRoles = this.getUserRoles();
    if (!userRoles) {
      return false;
    }

    return roles.some(role => userRoles.includes(role));
  }

  updateCurrentUserInfo() {
    this.isLoggedIn(); // triggering currentUser update, without really needing the bool result
  }
  
  refreshToken(refreshToken: string) {
    localStorage.removeItem('authToken');
    localStorage.setItem('authToken', refreshToken)
  }
  
  login(user: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Login`, user);
  }

  register(credentials: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Register`, credentials);
  }

  forgotPassword(email: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ForgottenPassword`, email);
  }

  resetPassword(resetModel: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ResetPassword`, resetModel);
  }

  confirmAccount(accountConfirmationHash: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ConfirmAccount`, accountConfirmationHash);
  }

}
