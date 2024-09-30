import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientCarRepairDetailComponent } from './client-car-repair-detail.component';

describe('ClientCarRepairDetailComponent', () => {
  let component: ClientCarRepairDetailComponent;
  let fixture: ComponentFixture<ClientCarRepairDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientCarRepairDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientCarRepairDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
