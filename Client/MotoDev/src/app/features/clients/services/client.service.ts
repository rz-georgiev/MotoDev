import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientResponse } from '../../client-car-repairs/models/clientResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  private baseUrl = `${Urls.ApiUrl}/Clients`;

  constructor(private http: HttpClient) { }

  getClients(): Observable<BaseResponse<ClientResponse[]>> {
    return this.http.get<BaseResponse<ClientResponse[]>>(`${this.baseUrl}/GetAllClients`);
  }

}
