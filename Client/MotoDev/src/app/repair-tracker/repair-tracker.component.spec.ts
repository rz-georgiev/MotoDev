import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepairTrackerComponent } from './repair-tracker.component';

describe('RepairTrackerComponent', () => {
  let component: RepairTrackerComponent;
  let fixture: ComponentFixture<RepairTrackerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RepairTrackerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepairTrackerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
