import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpravljanjeArtiklimaComponent } from './upravljanje-artiklima.component';

describe('UpravljanjeArtiklimaComponent', () => {
  let component: UpravljanjeArtiklimaComponent;
  let fixture: ComponentFixture<UpravljanjeArtiklimaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpravljanjeArtiklimaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpravljanjeArtiklimaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
