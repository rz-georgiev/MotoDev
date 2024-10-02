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
import { CarRepairRequest } from '../models/carRepairRequest';
import { ClientCarEditResponse } from '../models/clientCarEditResponse';
import { CarRepairSelectResponse } from '../models/carRepairSelectResponse';

@Injectable({
  providedIn: 'root'
})
export class CarRepairService {

  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getAllCarsRepairs(): Observable<BaseResponse<CarRepairResponse[]>> {
    return this.http.get<BaseResponse<CarRepairResponse[]>>(`${this.baseUrl}/CarRepairs/GetAllCarsRepairs`);
  }

  getById(id: number): Observable<BaseResponse<ClientCarEditResponse>> {
    return this.http.get<BaseResponse<ClientCarEditResponse>>(`${this.baseUrl}/CarRepairs/GetById?carRepairId=${id}`);
  }

  editCarRepair(carRepairData: CarRepairRequest): Observable<BaseResponse<CarRepairResponse>> {
    return this.http.post<BaseResponse<CarRepairResponse>>(`${this.baseUrl}/CarRepairs/Edit`, carRepairData);
  }

  deactivateByCarRepairIdAsync(carRepairId: number): Observable<BaseResponse<boolean>> {
    return this.http.put<BaseResponse<boolean>>(`${this.baseUrl}/CarRepairs/DeactivateByCarRepairId?carRepairId=${carRepairId}`, null);
  }

  getClientsRepairs(): Observable<BaseResponse<CarRepairSelectResponse[]>> {
    return this.http.get<BaseResponse<CarRepairSelectResponse[]>>(`${this.baseUrl}/CarRepairs/GetClientsRepairs`);
  }
}