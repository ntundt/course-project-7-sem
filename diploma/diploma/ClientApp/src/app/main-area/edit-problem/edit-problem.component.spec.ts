import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProblemComponent } from './edit-problem.component';

describe('EditProblemComponent', () => {
  let component: EditProblemComponent;
  let fixture: ComponentFixture<EditProblemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditProblemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditProblemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
