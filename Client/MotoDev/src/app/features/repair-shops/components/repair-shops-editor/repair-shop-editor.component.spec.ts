import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepairShopEditorComponent } from './repair-shop-editor.component';

describe('RepairShopEditorComponent', () => {
  let component: RepairShopEditorComponent;
  let fixture: ComponentFixture<RepairShopEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RepairShopEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepairShopEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
