import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../shared/models/baseResponse';
import { ClientCarStatusResponse } from '../models/clientCarStatusResponse';

@Injectable({
  providedIn: 'root'
})
export class RepairTrackerService {
  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getMyCarsStatusesAsync(): Observable<BaseResponse<ClientCarStatusResponse[]>> {
    return this.http.get<BaseResponse<ClientCarStatusResponse[]>>(`${this.baseUrl}/Clients/GetMyCarsStatuses`);
  }
}
