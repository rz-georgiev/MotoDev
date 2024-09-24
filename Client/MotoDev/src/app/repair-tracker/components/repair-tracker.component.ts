import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RepairTrackerService } from '../services/repair-tracker.service';

@Component({
  selector: 'app-repair-tracker',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './repair-tracker.component.html',
  styleUrl: './repair-tracker.component.css'
})
export class RepairTrackerComponent {
  constructor(private repairTrackerService: RepairTrackerService) {
    this.repairTrackerService.getMyCarsStatusesAsync().subscribe(x => {
      console.log(x);
    });
  }

  ngOnInit() {
  
  }
}
