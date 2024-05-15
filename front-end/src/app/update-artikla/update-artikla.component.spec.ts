import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateArtiklaComponent } from './update-artikla.component';

describe('UpdateArtiklaComponent', () => {
  let component: UpdateArtiklaComponent;
  let fixture: ComponentFixture<UpdateArtiklaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateArtiklaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateArtiklaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
