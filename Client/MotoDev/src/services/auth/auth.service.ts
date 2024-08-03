import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'https://localhost:7204/Account'; // Replace with your API URL 

  constructor(private httpClient: HttpClient) {}

  login(user: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Login`, user);
  }

  register(credentials: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/Register`, credentials);
  }

  forgotPassword(email: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/ForgottenPassword`, email);
  }

}
