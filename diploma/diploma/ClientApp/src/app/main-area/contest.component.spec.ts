import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContestComponent } from './contest.component';

describe('MainAreaComponent', () => {
  let component: ContestComponent;
  let fixture: ComponentFixture<ContestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
