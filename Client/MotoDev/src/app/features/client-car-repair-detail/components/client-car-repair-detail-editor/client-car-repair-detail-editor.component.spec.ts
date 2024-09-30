import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientCarRepairDetailEditorComponent } from './client-car-repair-detail-editor.component';

describe('ClientCarRepairDetailEditorComponent', () => {
  let component: ClientCarRepairDetailEditorComponent;
  let fixture: ComponentFixture<ClientCarRepairDetailEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientCarRepairDetailEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientCarRepairDetailEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
