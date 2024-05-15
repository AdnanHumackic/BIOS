import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DodavanjeRadnikaComponent } from './dodavanje-radnika.component';

describe('DodavanjeRadnikaComponent', () => {
  let component: DodavanjeRadnikaComponent;
  let fixture: ComponentFixture<DodavanjeRadnikaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DodavanjeRadnikaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DodavanjeRadnikaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
