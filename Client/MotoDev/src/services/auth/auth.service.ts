import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';
import { ExtendedJwtPayload } from '../../interfaces/extendedJwtPayload';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'https://localhost:7204/Account'; // Replace with your API URL 

  constructor(private httpClient: HttpClient) {}

  isLoggedIn(): boolean {
    const authToken = localStorage.getItem('authToken');
    if (!authToken) {
      return false;
    }

    const decoded = this.getDecodedToken(authToken);
    const date = new Date(0);
    date.setUTCSeconds(decoded.exp ?? 0);
    const hasExpired = date.valueOf() <= Date.now().valueOf();
    
    return !!authToken && !hasExpired;
  }

  getUserRoles(): string[] {
    const authToken = localStorage.getItem('authToken');
    if (!authToken){
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
    if (!userRoles){
      return false;
    }
    
    return roles.some(role => userRoles.includes(role));
  }

  login(user: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Login`, user);
  }

  register(credentials: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Register`, credentials);
  }

  forgotPassword(email: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ForgottenPassword`, email);
  }

  resetPassword(resetModel: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ResetPassword`, resetModel);
  }

  confirmAccount(accountConfirmationHash: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ConfirmAccount`, accountConfirmationHash);
  }

}
