import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateKompatibilnihComponent } from './update-kompatibilnih.component';

describe('UpdateKompatibilnihComponent', () => {
  let component: UpdateKompatibilnihComponent;
  let fixture: ComponentFixture<UpdateKompatibilnihComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateKompatibilnihComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateKompatibilnihComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
