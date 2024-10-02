import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { Urls } from '../../../shared/consts/urls';


@Injectable({
  providedIn: 'root'
})
export class SidebarService {

  private baseUrl =  `${Urls.ApiUrl}/Account`; 

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();
  
  constructor(private httpClient: HttpClient) {}

}
