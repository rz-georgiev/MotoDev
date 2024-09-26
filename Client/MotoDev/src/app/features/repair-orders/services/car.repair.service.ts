import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { CarRepairResponse } from '../models/carRepairResponse';
import { UserDto } from '../../users/models/userDto';
import { UserDtoMinimized } from '../../users/models/userDtoMinimized';
import { UserExtendedDto } from '../../users/models/UserExtendedDto';
import { UserProfileImageUpdateResponse } from '../../users/models/userProfileImageUpdateResponse';
import { ClientResponse } from '../models/clientResponse';
import { ClientCarResponse } from '../models/clientCarResponse';

@Injectable({
  providedIn: 'root'
})
export class CarRepairService {

  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getAllCarsRepairs(): Observable<BaseResponse<CarRepairResponse[]>> {
    return this.http.get<BaseResponse<CarRepairResponse[]>>(`${this.baseUrl}/CarRepairs/GetAllCarsRepairs`);
  }

  getClients(): Observable<BaseResponse<ClientResponse[]>> {
    return this.http.get<BaseResponse<ClientResponse[]>>(`${this.baseUrl}/Clients/GetAllClients`);
  }

  getClientCars(clientId: number): Observable<BaseResponse<ClientCarResponse[]>> {
    return this.http.get<BaseResponse<ClientCarResponse[]>>(`${this.baseUrl}/ClientCars/GetClientCars?clientId=${clientId}`);
  }


  getById(id: number): Observable<BaseResponse<UserDto>> {
    return this.http.get<BaseResponse<UserDto>>(`${this.baseUrl}/Users/GetById?id=${id}`);
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
