import { TestBed } from '@angular/core/testing';

import { ClientCarService } from './client-car.service';

describe('ClientCarService', () => {
  let service: ClientCarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClientCarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
