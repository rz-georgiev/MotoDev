import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { ExtendedJwtPayload } from '../../auth/models/extendedJwtPayload';
import { UserResponse } from '../models/userResponse';
import { AuthService } from '../../auth/services/auth.service';
import { UserDto } from '../models/userDto';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { UserProfileImageUpdateResponse } from '../models/userProfileImageUpdateResponse';
import { UserDtoMinimized } from '../models/userDtoMinimized';
import { UserExtendedDto } from '../models/UserExtendedDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient,
    private authService: AuthService) { }

  getById(id: number): Observable<BaseResponse<UserDto>> {
    return this.http.get<BaseResponse<UserDto>>(`${this.baseUrl}/Users/GetById?id=${id}`);
  }

  getAllForCurrentOwnerUserId(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Users/GetAllForCurrentOwnerUserId?ownerUserId=${this.authService.currentUser.id}`);
  }

  deactivateRepairUserById(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/Users/DeactivateRepairUserById?id=${id}`, null);
  }

  editUser(userData: UserDto): Observable<BaseResponse<UserDto>> {
    return this.http.post<BaseResponse<UserDto>>(`${this.baseUrl}/Users/Edit`, userData);
  }

  // Used when the user is editing his/her information themselves
  editUserMinimized(userData: UserDtoMinimized): Observable<BaseResponse<UserExtendedDto>> {
    return this.http.post<BaseResponse<UserExtendedDto>>(`${this.baseUrl}/Users/EditMinimized`, userData);
  }

  updateProfileImage(formData: FormData): Observable<BaseResponse<UserProfileImageUpdateResponse>> {
    return this.http.post<BaseResponse<UserProfileImageUpdateResponse>>(`${this.baseUrl}/Users/UpdateProfileImage`, formData);
  }
  

}
