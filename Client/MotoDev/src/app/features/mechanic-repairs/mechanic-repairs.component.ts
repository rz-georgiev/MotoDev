import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BaseResponse } from '../../shared/models/baseResponse';
import { RepairStatusOption } from '../../shared/consts/repairStatusOption';
import { UtcToLocalPipe } from '../../core/pipes/utc-to-local.pipe';
import { ClientCarStatusResponse } from '../repair-tracker/models/clientCarStatusResponse';
import { RepairTrackerService } from '../repair-tracker/services/repair-tracker.service';

@Component({
  selector: 'app-mechanic-repairs',
  standalone: true,
  imports: [CommonModule, UtcToLocalPipe],
  templateUrl: './mechanic-repairs.component.html',
  styleUrl: './mechanic-repairs.component.css'
})
export class MechanicRepairsComponent {
  public response!: ClientCarStatusResponse[];
  RepairStatusOption = RepairStatusOption;
    
  constructor(private repairTrackerService: RepairTrackerService) {
   
  }

  ngOnInit() {
    this.repairTrackerService.getMyCarsStatusesAsync().subscribe(x => {
      this.response = x.result;
    });
  }
}
