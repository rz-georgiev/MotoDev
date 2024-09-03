import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RepairShopService {

  private baseUrl = `https://localhost:5078`; 

  constructor(private http: HttpClient) { }

  getRepairShopsForSpecifiedOwner(ownerUserId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/RepairShops/GetForSpecifiedOwner?ownerUserId=${ownerUserId}`);
  }
}
