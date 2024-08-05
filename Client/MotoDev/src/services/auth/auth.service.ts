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
    return !!authToken;
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
  
  hasRole(role: string): boolean {
    return this.getUserRoles().includes(role);
  }

  login(user: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Login`, user);
  }

  logout() {
    localStorage.removeItem('authToken');
  }

  register(credentials: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Register`, credentials);
  }

  forgotPassword(email: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ForgottenPassword`, email);
  }

}
