import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientCarComponent } from './client-car.component';

describe('ClientCarComponent', () => {
  let component: ClientCarComponent;
  let fixture: ComponentFixture<ClientCarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientCarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientCarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
