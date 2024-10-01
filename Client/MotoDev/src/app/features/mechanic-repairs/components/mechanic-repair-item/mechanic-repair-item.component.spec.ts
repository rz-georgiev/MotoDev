import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MechanicRepairItemComponent } from './mechanic-repair-item.component';

describe('MechanicRepairItemComponent', () => {
  let component: MechanicRepairItemComponent;
  let fixture: ComponentFixture<MechanicRepairItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MechanicRepairItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MechanicRepairItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
