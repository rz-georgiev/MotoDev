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
import { MechanicUserResponse } from '../models/mechanicUserResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = `${Urls.ApiUrl}/Users`;

  constructor(private http: HttpClient,
    private authService: AuthService) { }

  getById(id: number): Observable<BaseResponse<UserDto>> {
    return this.http.get<BaseResponse<UserDto>>(`${this.baseUrl}/GetById?id=${id}`);
  }

  getAllForCurrentOwnerUserId(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetAllForCurrentOwnerUserId?ownerUserId=${this.authService.currentUser.id}`);
  }

  deactivateRepairUserById(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/DeactivateRepairUserById?id=${id}`, null);
  }

  editUser(userData: UserDto): Observable<BaseResponse<UserDto>> {
    return this.http.post<BaseResponse<UserDto>>(`${this.baseUrl}/Edit`, userData);
  }

  // Used when the user is editing his/her information themselves
  editUserMinimized(userData: UserDtoMinimized): Observable<BaseResponse<UserExtendedDto>> {
    return this.http.post<BaseResponse<UserExtendedDto>>(`${this.baseUrl}/EditMinimized`, userData);
  }

  updateProfileImage(formData: FormData): Observable<BaseResponse<UserProfileImageUpdateResponse>> {
    return this.http.post<BaseResponse<UserProfileImageUpdateResponse>>(`${this.baseUrl}/UpdateProfileImage`, formData);
  }
  
  getMechanicUsers(): Observable<BaseResponse<MechanicUserResponse[]>> {
    return this.http.get<BaseResponse<MechanicUserResponse[]>>(`${this.baseUrl}/GetMechanicUsers`);
  }
  

}
