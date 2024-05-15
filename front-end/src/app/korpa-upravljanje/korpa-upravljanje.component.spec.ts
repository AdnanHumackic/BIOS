import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KorpaUpravljanjeComponent } from './korpa-upravljanje.component';

describe('KorpaUpravljanjeComponent', () => {
  let component: KorpaUpravljanjeComponent;
  let fixture: ComponentFixture<KorpaUpravljanjeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KorpaUpravljanjeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KorpaUpravljanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
