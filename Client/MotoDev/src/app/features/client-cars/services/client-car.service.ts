import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientCarResponse } from '../../client-car-repairs/models/clientCarResponse';
import { ClientCarListingReponse } from '../models/clientCarListingResponse';
import { ClientCarEditDto } from '../models/clientCarEditDto';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class ClientCarService {

  private baseUrl = `${Urls.ApiUrl}/ClientCars`;

  constructor(private http: HttpClient) { }

  getAllClientCars(): Observable<BaseResponse<ClientCarListingReponse[]>> {
    return this.http.get<BaseResponse<ClientCarListingReponse[]>>(`${this.baseUrl}/GetAllClientCars`);
  }
  
  getById(clientCarId: number): Observable<BaseResponse<ClientCarEditDto>> {
    return this.http.get<BaseResponse<ClientCarEditDto>>(`${this.baseUrl}/GetById?clientCarId=${clientCarId}`);
  }

  edit(data: ClientCarEditDto): Observable<BaseResponse<ClientCarListingReponse>> {
    return this.http.post<BaseResponse<ClientCarListingReponse>>(`${this.baseUrl}/Edit`, data);
  }
  
  deactivateByClientCarId(clientCarId: number): Observable<BaseResponse<ClientCarResponse[]>> {
    return this.http.put<BaseResponse<ClientCarResponse[]>>(`${this.baseUrl}/DeactivateByClientCarId?clientCarId=${clientCarId}`, null);
  }

  getClientCars(clientId: number): Observable<BaseResponse<ClientCarResponse[]>> {
    return this.http.get<BaseResponse<ClientCarResponse[]>>(`${this.baseUrl}/GetClientCars?clientId=${clientId}`);
  }
  
}
