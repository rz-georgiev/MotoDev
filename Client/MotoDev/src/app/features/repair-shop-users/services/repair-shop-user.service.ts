import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepairShopUserDto } from '../models/repairShopUser';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class RepairShopUserService {

  private baseUrl = `${Urls.ApiUrl}/RepairShopUsers`; 

  constructor(private http: HttpClient) { }

  getRepairShopsForCurrentUser(): Observable<BaseResponse<RepairShopUserDto[]>> {
    return this.http.get<BaseResponse<RepairShopUserDto[]>>(`${this.baseUrl}/GetRepairShopsForCurrentUser`);
  }

  getRepairShopUserById(id: number): Observable<BaseResponse<RepairShopUserDto>> {
    return this.http.get<BaseResponse<RepairShopUserDto>>(`${this.baseUrl}/GetById?id=${id}`);
  }

}
