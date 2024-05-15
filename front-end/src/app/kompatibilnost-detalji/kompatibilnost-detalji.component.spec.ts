import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KompatibilnostDetaljiComponent } from './kompatibilnost-detalji.component';

describe('KompatibilnostDetaljiComponent', () => {
  let component: KompatibilnostDetaljiComponent;
  let fixture: ComponentFixture<KompatibilnostDetaljiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KompatibilnostDetaljiComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KompatibilnostDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
