import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RepairTrackerService } from '../services/repair-tracker.service';
import { ClientCarStatusResponse } from '../models/clientCarStatusResponse';
import { BaseResponse } from '../../shared/models/baseResponse';
import { RepairStatusOption } from '../../shared/consts/repairStatusOption';
import { UtcToLocalPipe } from '../../core/pipes/utc-to-local.pipe';

@Component({
  selector: 'app-repair-tracker',
  standalone: true,
  imports: [CommonModule, UtcToLocalPipe],
  templateUrl: './repair-tracker.component.html',
  styleUrl: './repair-tracker.component.css'
})
export class RepairTrackerComponent {

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
