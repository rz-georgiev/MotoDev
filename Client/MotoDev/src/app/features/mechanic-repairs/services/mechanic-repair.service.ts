import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../../repair-shop-users/models/repairShopUser';
import { RepairShopDto } from '../../repair-shops/models/repairShopDto';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { MechanicRepairResponse } from '../models/mechanicRepairReponse';
import { MechanicDetailUpdateRequest } from '../models/mechanicDetailUpdateRequest';

@Injectable({
  providedIn: 'root'
})
export class MechanicRepairService {
  private baseUrl = `https://localhost:5078`; 

  constructor(private http: HttpClient) { }

  getLastTenOrdersAsync(): Observable<BaseResponse<MechanicRepairResponse[]>> {
    return this.http.get<BaseResponse<MechanicRepairResponse[]>>(`${this.baseUrl}/MechanicRepairs/GetLastTenOrders`);
  }

  updateDetail(request: MechanicDetailUpdateRequest): Observable<BaseResponse<boolean[]>> {
    return this.http.put<BaseResponse<boolean[]>>(`${this.baseUrl}/MechanicRepairs/UpdateDetail`, request);
  }

}
