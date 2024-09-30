import { TestBed } from '@angular/core/testing';

import { CarRepairDetailService } from './car-repair-detail.service';

describe('ClientRepairDetailService', () => {
  let service: CarRepairDetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarRepairDetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
