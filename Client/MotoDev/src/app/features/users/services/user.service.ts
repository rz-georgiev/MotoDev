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

  
  private apiUrl = 'https://localhost:5078/Users/GetAllForCurrentOwnerUserId?ownerUserId=8'; // Example API

  constructor(private http: HttpClient) { }

  getData(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
