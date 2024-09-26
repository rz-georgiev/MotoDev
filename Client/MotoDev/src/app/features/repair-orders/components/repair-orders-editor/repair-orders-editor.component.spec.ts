import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepairOrdersEditorComponent } from './repair-orders-editor.component';

describe('RepairOrdersEditorComponent', () => {
  let component: RepairOrdersEditorComponent;
  let fixture: ComponentFixture<RepairOrdersEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RepairOrdersEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepairOrdersEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
