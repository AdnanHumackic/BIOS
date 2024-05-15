import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAkcijeComponent } from './admin-akcije.component';

describe('AdminAkcijeComponent', () => {
  let component: AdminAkcijeComponent;
  let fixture: ComponentFixture<AdminAkcijeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminAkcijeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminAkcijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
