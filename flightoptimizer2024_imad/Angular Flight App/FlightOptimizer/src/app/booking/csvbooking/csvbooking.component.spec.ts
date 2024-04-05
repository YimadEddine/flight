import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CSVbookingComponent } from './csvbooking.component';

describe('CSVbookingComponent', () => {
  let component: CSVbookingComponent;
  let fixture: ComponentFixture<CSVbookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CSVbookingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CSVbookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
