import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacProfilEditComponent } from './kupac-profil-edit.component';

describe('KupacProfilEditComponent', () => {
  let component: KupacProfilEditComponent;
  let fixture: ComponentFixture<KupacProfilEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacProfilEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacProfilEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
