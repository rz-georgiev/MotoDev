import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BaseResponse } from '../../../../shared/models/baseResponse';
import { RepairStatusOption } from '../../../../shared/consts/repairStatusOption';
import { UtcToLocalPipe } from '../../../../core/pipes/utc-to-local.pipe';
import { ClientCarStatusResponse } from '../../../repair-tracker/models/clientCarStatusResponse';
import { RepairTrackerService } from '../../../repair-tracker/services/repair-tracker.service';
import { MechanicRepairItemComponent } from "../mechanic-repair-item/mechanic-repair-item.component";
import { MechanicRepairService } from '../../services/mechanic-repair.service';
import { MechanicRepairResponse } from '../../models/mechanicRepairReponse';
import { MechanicDetailUpdateRequest } from '../../models/mechanicDetailUpdateRequest';
import { RoleOption } from '../../../../shared/consts/roleOption';
import { MechanicRepairResponseDetail } from '../../models/mechanicRepairReponseDetail';

@Component({
  selector: 'app-mechanic-repairs',
  standalone: true,
  imports: [CommonModule, UtcToLocalPipe, MechanicRepairItemComponent],
  templateUrl: './mechanic-repairs.component.html',
  styleUrl: './mechanic-repairs.component.css'
})
export class MechanicRepairsComponent {


  public response!: ClientCarStatusResponse[];
  public data!: MechanicRepairResponse[];
  RepairStatusOption = RepairStatusOption;

  constructor(private repairTrackerService: RepairTrackerService,
    private mechanicRepairService: MechanicRepairService
  ) {

  }

  ngOnInit() {
    this.repairTrackerService.getMyCarsStatusesAsync().subscribe(x => {
      this.response = x.result;

      this.mechanicRepairService.getLastTenOrdersAsync().subscribe(x => {
        this.data = x.result;
      });
    });
  }

  onFocusOut(detail: MechanicRepairResponseDetail, $event: any) {
    const newNote = $event.currentTarget.value;
    this.mechanicRepairService.updateDetail({
      repairDetailId: detail.repairDetailId, 
      newStatusId: detail.statusId,
      newNotes: newNote
    }).subscribe();
  }

  onStatusChangerClick(detail: MechanicRepairResponseDetail, newStatusId: number) {
    detail.statusId = newStatusId;
    this.mechanicRepairService.updateDetail({
      repairDetailId: detail.repairDetailId, 
      newStatusId: newStatusId,
      newNotes: detail.notes
    }).subscribe();
  }

}
