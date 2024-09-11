import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../models/repairShopUser';
import { BaseResponse } from '../../../shared/models/baseResponse';

@Injectable({
  providedIn: 'root'
})
export class RepairShopUserService {

  private baseUrl = `https://localhost:5078`; 

  constructor(private http: HttpClient) { }

  getRepairShopsForUserId(userId: number): Observable<BaseResponse<RepairShopUserDto[]>> {
    return this.http.get<BaseResponse<RepairShopUserDto[]>>(`${this.baseUrl}/RepairShopUsers/GetRepairShopsForUserId?userId=${userId}`);
  }

  getRepairShopUserById(id: number): Observable<BaseResponse<RepairShopUserDto>> {
    return this.http.get<BaseResponse<RepairShopUserDto>>(`${this.baseUrl}/RepairShopUsers/GetById?id=${id}`);
  }
}
