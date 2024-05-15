import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledArtikalaComponent } from './pregled-artikala.component';

describe('PregledArtikalaComponent', () => {
  let component: PregledArtikalaComponent;
  let fixture: ComponentFixture<PregledArtikalaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PregledArtikalaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PregledArtikalaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
