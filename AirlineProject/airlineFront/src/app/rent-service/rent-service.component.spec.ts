import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RentServiceComponent } from './rent-service.component';

describe('RentServiceComponent', () => {
  let component: RentServiceComponent;
  let fixture: ComponentFixture<RentServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RentServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RentServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
