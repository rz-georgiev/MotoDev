import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientCarStatusResponse } from '../models/clientCarStatusResponse';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class RepairTrackerService {
  private baseUrl = `${Urls.ApiUrl}/Clients`;

  constructor(private http: HttpClient) { }

  getMyCarsStatusesAsync(): Observable<BaseResponse<ClientCarStatusResponse[]>> {
    return this.http.get<BaseResponse<ClientCarStatusResponse[]>>(`${this.baseUrl}/GetMyCarsStatuses`);
  }
}
