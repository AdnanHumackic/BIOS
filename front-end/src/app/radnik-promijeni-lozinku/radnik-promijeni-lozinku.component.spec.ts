import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RadnikPromijeniLozinkuComponent } from './radnik-promijeni-lozinku.component';

describe('RadnikPromijeniLozinkuComponent', () => {
  let component: RadnikPromijeniLozinkuComponent;
  let fixture: ComponentFixture<RadnikPromijeniLozinkuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RadnikPromijeniLozinkuComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RadnikPromijeniLozinkuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
