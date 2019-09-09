import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowsPlacesComponent } from './shows-places.component';

describe('ShowsPlacesComponent', () => {
  let component: ShowsPlacesComponent;
  let fixture: ComponentFixture<ShowsPlacesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowsPlacesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowsPlacesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
