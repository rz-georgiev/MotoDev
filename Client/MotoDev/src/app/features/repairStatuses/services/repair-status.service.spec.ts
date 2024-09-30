import { TestBed } from '@angular/core/testing';

import { RepairStatusService } from './repair-status.service';

describe('RepairStatusService', () => {
  let service: RepairStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RepairStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
