import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepairOrdersComponent } from './repair-orders.component';

describe('RepairOrdersComponent', () => {
  let component: RepairOrdersComponent;
  let fixture: ComponentFixture<RepairOrdersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RepairOrdersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepairOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
