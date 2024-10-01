import { TestBed } from '@angular/core/testing';

import { MechanicRepairService } from './mechanic-repair.service';

describe('MechanicRepairService', () => {
  let service: MechanicRepairService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MechanicRepairService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
