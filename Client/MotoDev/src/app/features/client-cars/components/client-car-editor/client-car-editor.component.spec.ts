import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientCarEditorComponent } from './client-car-editor.component';

describe('ClientCarEditorComponent', () => {
  let component: ClientCarEditorComponent;
  let fixture: ComponentFixture<ClientCarEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientCarEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientCarEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
