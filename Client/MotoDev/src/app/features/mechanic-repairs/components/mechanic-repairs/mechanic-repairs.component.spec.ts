import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MechanicRepairsComponent } from './mechanic-repairs.component';

describe('MechanicRepairsComponent', () => {
  let component: MechanicRepairsComponent;
  let fixture: ComponentFixture<MechanicRepairsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MechanicRepairsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MechanicRepairsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
