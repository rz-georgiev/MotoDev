import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../../repair-shop-users/models/repairShopUser';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { RepairShopDto } from '../models/repairShopDto';

@Injectable({
  providedIn: 'root'
})
export class RepairShopService {

  private baseUrl = `https://localhost:5078`; 

  constructor(private http: HttpClient) { }

  getById(id: number): Observable<BaseResponse<RepairShopDto>> {
    return this.http.get<BaseResponse<RepairShopDto>>(`${this.baseUrl}/RepairShops/GetById/${id}`);
  }

  getRepairShopsForSpecifiedOwner(ownerUserId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/RepairShops/GetForSpecifiedOwner?ownerUserId=${ownerUserId}`);
  }

  getRepairShopUserById(id: number): Observable<BaseResponse<RepairShopUserDto>> {
    return this.http.get<BaseResponse<RepairShopUserDto>>(`${this.baseUrl}/RepairShopUsers/GetById?id=${id}`);
  }

  getForSpecifiedIds(ids: number[]): Observable<BaseResponse<RepairShopDto[]>> {
    let params = new HttpParams();
    ids.forEach(id => {
      params = params.append('repairShopsIds', id);
    })
    return this.http.get<BaseResponse<RepairShopDto[]>>(`${this.baseUrl}/RepairShops/GetForSpecifiedIds`, { params });
  }
}
