import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DodavanjeKompatibilnihComponent } from './dodavanje-kompatibilnih.component';

describe('DodavanjeKompatibilnihComponent', () => {
  let component: DodavanjeKompatibilnihComponent;
  let fixture: ComponentFixture<DodavanjeKompatibilnihComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DodavanjeKompatibilnihComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DodavanjeKompatibilnihComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
