import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientCarResponse } from '../../repair-orders/models/clientCarResponse';

@Injectable({
  providedIn: 'root'
})
export class ClientCarService {

  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getClientCars(clientId: number): Observable<BaseResponse<ClientCarResponse[]>> {
    return this.http.get<BaseResponse<ClientCarResponse[]>>(`${this.baseUrl}/ClientCars/GetClientCars?clientId=${clientId}`);
  }
  
}
