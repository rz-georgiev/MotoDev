import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AppRoutingModule } from '../../../app.routes';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../features/auth/services/auth.service';
import { ItemVisibility } from '../models/itemVisibility';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  
  public openedTabName: string = "Dashboard";
  public visibleTabs: ItemVisibility[] = [];
  public currentUserRole!: string[];

  constructor(public authService: AuthService) { }

  ngOnInit() {

    this.currentUserRole = this.authService.getUserRoles();

    this.visibleTabs = [{
      roleName: 'Owner',
      itemsNames: ['Dashboard', 'Users', 'Settings', 'Cars', 'RepairShops', 'BaseData', 'About'],
    },
    {
      roleName: 'Mechanic',
      itemsNames: ['Repairs', 'About'],
    }
    ,
    {
      roleName: 'Client',
      itemsNames: ['RepairTracker', 'About'],
    }
   ];
  }

  public toggleTab(tabName: string) {
    this.openedTabName = tabName;
  }
}
