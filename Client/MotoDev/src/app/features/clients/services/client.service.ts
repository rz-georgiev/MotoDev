import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientResponse } from '../../client-car-repairs/models/clientResponse';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getClients(): Observable<BaseResponse<ClientResponse[]>> {
    return this.http.get<BaseResponse<ClientResponse[]>>(`${this.baseUrl}/Clients/GetAllClients`);
  }

}
