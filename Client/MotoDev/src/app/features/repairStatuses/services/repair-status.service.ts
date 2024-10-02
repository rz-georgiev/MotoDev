import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientCarResponse } from '../../client-car-repairs/models/clientCarResponse';
import { ClientCarEditDto } from '../../client-cars/models/clientCarEditDto';
import { ClientCarListingReponse } from '../../client-cars/models/clientCarListingResponse';
import { RepairTypeResponse } from '../../repairTypes/models/repairTypeResponse';
import { RepairStatusResponse } from '../models/repairStatusResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class RepairStatusService {

 
  private baseUrl = `${Urls.ApiUrl}/RepairStatuses`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<BaseResponse<RepairStatusResponse[]>> {
    return this.http.get<BaseResponse<RepairStatusResponse[]>>(`${this.baseUrl}/GetAll`);
  }
}
