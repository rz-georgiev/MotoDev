import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../../repair-shop-users/models/repairShopUser';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { DashboardResponse } from '../models/dashboardResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private baseUrl = `${Urls.ApiUrl}/Dashboard`;

  constructor(private http: HttpClient) { }

  getDashboardData(): Observable<BaseResponse<DashboardResponse>> {
    return this.http.get<BaseResponse<DashboardResponse>>(`${this.baseUrl}/GetDashboardData`);
  }

}
