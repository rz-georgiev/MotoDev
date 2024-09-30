import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientCarResponse } from '../../client-car-repairs/models/clientCarResponse';
import { ClientCarEditDto } from '../../client-cars/models/clientCarEditDto';
import { ClientCarListingReponse } from '../../client-cars/models/clientCarListingResponse';
import { CarRepairDetailListingResponse } from '../models/carRepairDetailListingResponse';
import { CarRepairDetailEditDto } from '../models/carRepairDetailEditDto';

@Injectable({
  providedIn: 'root'
})
export class CarRepairDetailService {
  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<BaseResponse<CarRepairDetailListingResponse[]>> {
    return this.http.get<BaseResponse<CarRepairDetailListingResponse[]>>(`${this.baseUrl}/CarRepairDetails/GetAll`);
  }
  
  getById(detailId: number): Observable<BaseResponse<CarRepairDetailEditDto>> {
    return this.http.get<BaseResponse<CarRepairDetailEditDto>>(`${this.baseUrl}/CarRepairDetails/GetById?detailId=${detailId}`);
  }

  edit(data: CarRepairDetailEditDto): Observable<BaseResponse<CarRepairDetailListingResponse>> {
    return this.http.post<BaseResponse<CarRepairDetailListingResponse>>(`${this.baseUrl}/CarRepairDetails/Edit`, data);
  }
  
  deactivateByCientCarRepairDetailId(detailId: number): Observable<BaseResponse<CarRepairDetailListingResponse[]>> {
    return this.http.put<BaseResponse<CarRepairDetailListingResponse[]>>(`${this.baseUrl}/CarRepairDetails/DeactivateByDetailId?detailId=${detailId}`, null);
  }

}
