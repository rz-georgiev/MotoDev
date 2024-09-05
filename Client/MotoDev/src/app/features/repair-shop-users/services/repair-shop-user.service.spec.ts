import { TestBed } from '@angular/core/testing';

import { RepairShopUserService } from './repair-shop-user.service';

describe('RepairShopService', () => {
  let service: RepairShopUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RepairShopUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
