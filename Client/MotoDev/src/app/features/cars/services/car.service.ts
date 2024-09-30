import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../../shared/models/baseResponse';
import { CarRepairRequest } from '../../client-car-repairs/models/carRepairRequest';
import { CarRepairResponse } from '../../client-car-repairs/models/carRepairResponse';
import { ClientCarEditResponse } from '../../client-car-repairs/models/clientCarEditResponse';
import { CarResponse } from '../models/carResponse';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  private baseUrl = `https://localhost:5078`;

  constructor(private http: HttpClient) { }

  getAllCars(): Observable<BaseResponse<CarResponse[]>> {
    return this.http.get<BaseResponse<CarResponse[]>>(`${this.baseUrl}/Cars/GetAll`);
  }

}
