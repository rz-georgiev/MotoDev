import { TestBed } from '@angular/core/testing';

import { RepairTrackerService } from './repair-tracker.service';

describe('RepairTrackerService', () => {
  let service: RepairTrackerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RepairTrackerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
