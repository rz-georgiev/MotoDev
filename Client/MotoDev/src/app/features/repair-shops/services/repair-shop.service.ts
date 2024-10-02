import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../../repair-shop-users/models/repairShopUser';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { RepairShopDto } from '../models/repairShopDto';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class RepairShopService {

  private baseUrl = `${Urls.ApiUrl}/RepairShops`;

  constructor(private http: HttpClient) { }

  getById(id: number): Observable<BaseResponse<RepairShopDto>> {
    return this.http.get<BaseResponse<RepairShopDto>>(`${this.baseUrl}/GetById/${id}`);
  }

  getRepairShopsForSpecifiedOwner(ownerUserId: number): Observable<BaseResponse<RepairShopDto[]>> {
    return this.http.get<BaseResponse<RepairShopDto[]>>(`${this.baseUrl}/GetForSpecifiedOwner?ownerUserId=${ownerUserId}`);
  }

  getForSpecifiedIds(ids: number[]): Observable<BaseResponse<RepairShopDto[]>> {
    let params = new HttpParams();
    ids.forEach(id => {
      params = params.append('repairShopsIds', id);
    })
    return this.http.get<BaseResponse<RepairShopDto[]>>(`${this.baseUrl}/GetForSpecifiedIds`, { params });
  }

  edit(data: RepairShopDto): Observable<BaseResponse<RepairShopDto>> {
    return this.http.post<BaseResponse<RepairShopDto>>(`${this.baseUrl}/Edit`, data);
  }

  deactivateById(id: number): Observable<BaseResponse<boolean>> {
    return this.http.get<BaseResponse<boolean>>(`${this.baseUrl}/DeactivateById/${id}`);
  }
}
