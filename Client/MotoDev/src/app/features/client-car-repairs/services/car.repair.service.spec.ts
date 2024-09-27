import { TestBed } from '@angular/core/testing';

import { CarRepairService } from './car.repair.service';

describe('CarRepairService', () => {
  let service: CarRepairService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarRepairService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
