import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NeovlastenPristupComponent } from './neovlasten-pristup.component';

describe('NeovlastenPristupComponent', () => {
  let component: NeovlastenPristupComponent;
  let fixture: ComponentFixture<NeovlastenPristupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NeovlastenPristupComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NeovlastenPristupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
