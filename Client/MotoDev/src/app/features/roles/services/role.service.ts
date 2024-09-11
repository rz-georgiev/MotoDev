import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RoleDto } from '../models/roleDto';
import { BaseResponse } from '../../../shared/models/baseResponse';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private baseUrl = `https://localhost:5078`; 

  constructor(private http: HttpClient) { }

  getById(id: number): Observable<BaseResponse<RoleDto>> {
    return this.http.get<BaseResponse<RoleDto>>(`${this.baseUrl}/Roles/GetById?id=${id}`);
  }

  getAll(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Roles/GetAll`);
  }
}

