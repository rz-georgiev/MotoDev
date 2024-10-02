import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { ClientCarListingReponse } from '../../client-cars/models/clientCarListingResponse';
import { RepairTypeResponse } from '../models/repairTypeResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class RepairTypeService {

  private baseUrl = `${Urls.ApiUrl}/RepairTypes`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<BaseResponse<RepairTypeResponse[]>> {
    return this.http.get<BaseResponse<RepairTypeResponse[]>>(`${this.baseUrl}/GetAll`);
  }
}
