import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  private isSideBarOpened = new BehaviorSubject<boolean>(true);
  public isSideBarOpened$ = this.isSideBarOpened.asObservable();

  constructor() { }

   toggleSidebar() {
     this.isSideBarOpened.next(!this.isSideBarOpened.value);
  }
}
