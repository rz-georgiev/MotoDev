import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'https://localhost:7204'; // Replace with your API URL 

  constructor(private httpClient: HttpClient) {}

  login(user: any): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/login`, user);
  }

  register(credentials: any) : Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/register`, credentials);
  }

}
