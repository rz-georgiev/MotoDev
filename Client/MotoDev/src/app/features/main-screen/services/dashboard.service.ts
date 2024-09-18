import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../../repair-shop-users/models/repairShopUser';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { DashboardResponse } from '../models/dashboardResponse';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private baseUrl = `https://localhost:5078`; 

  constructor(private http: HttpClient) { }

  getDashboardData(): Observable<BaseResponse<DashboardResponse>> {
    return this.http.get<BaseResponse<DashboardResponse>>(`${this.baseUrl}/Dashboard/GetDashboardData`);
  }

}
