import { TestBed } from '@angular/core/testing';

import { RepairShopService } from './repair-shop.service';

describe('RepairShopService', () => {
  let service: RepairShopService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RepairShopService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
