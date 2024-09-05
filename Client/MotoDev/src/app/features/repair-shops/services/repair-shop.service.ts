import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../../repair-shop-users/models/repairShopUser';
import { BaseResponse } from '../../../shared/models/baseResponse';

@Injectable({
  providedIn: 'root'
})
export class RepairShopService {

  private baseUrl = `https://localhost:5078`; 

  constructor(private http: HttpClient) { }

  getRepairShopsForSpecifiedOwner(ownerUserId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/RepairShops/GetForSpecifiedOwner?ownerUserId=${ownerUserId}`);
  }

  getRepairShopUserById(id: number): Observable<BaseResponse<RepairShopUserDto>> {
    return this.http.get<BaseResponse<RepairShopUserDto>>(`${this.baseUrl}/RepairShopUsers/GetById?id=${id}`);
  }
}
