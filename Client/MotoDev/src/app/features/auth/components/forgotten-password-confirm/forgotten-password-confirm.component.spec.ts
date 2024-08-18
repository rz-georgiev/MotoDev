import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgottenPasswordConfirmComponent } from './forgotten-password-confirm.component';

describe('ForgottenPasswordConfirmComponent', () => {
  let component: ForgottenPasswordConfirmComponent;
  let fixture: ComponentFixture<ForgottenPasswordConfirmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForgottenPasswordConfirmComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForgottenPasswordConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
