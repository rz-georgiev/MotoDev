import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientCarResponse } from '../../client-car-repairs/models/clientCarResponse';
import { ClientCarEditDto } from '../../client-cars/models/clientCarEditDto';
import { ClientCarListingReponse } from '../../client-cars/models/clientCarListingResponse';
import { RepairTypeResponse } from '../../repairTypes/models/repairTypeResponse';
import { RepairStatusResponse } from '../models/repairStatusResponse';

@Injectable({
  providedIn: 'root'
})
export class RepairStatusService {

 
  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<BaseResponse<RepairStatusResponse[]>> {
    return this.http.get<BaseResponse<RepairStatusResponse[]>>(`${this.baseUrl}/RepairStatuses/GetAll`);
  }
}
