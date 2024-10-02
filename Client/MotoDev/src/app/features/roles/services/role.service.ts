import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RoleDto } from '../models/roleDto';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { Urls } from '../../../shared/consts/urls';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private baseUrl = `${Urls.ApiUrl}/Roles`;

  constructor(private http: HttpClient) { }

  getById(id: number): Observable<BaseResponse<RoleDto>> {
    return this.http.get<BaseResponse<RoleDto>>(`${this.baseUrl}/GetById?id=${id}`);
  }

  getAll(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetAll`);
  }
}

