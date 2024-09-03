import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { ExtendedJwtPayload } from '../../auth/models/extendedJwtPayload';
import { UserResponse } from '../models/userResponse';
import { AuthService } from '../../auth/services/auth.service';
import { UserDto } from '../models/userDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient,
    private authService: AuthService) { }

  getData(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Users/GetAllForCurrentOwnerUserId?ownerUserId=${this.authService.currentUserId}`);
  }

  deactivateRepairUserById(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/Users/DeactivateRepairUserById?id=${id}`, null);
  }

  createUser(userData: UserDto) : Observable<any> {
    return this.http.post(`${this.baseUrl}/Users/Create`, userData);
  }

}
