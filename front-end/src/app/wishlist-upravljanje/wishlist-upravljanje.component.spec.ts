import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WishlistUpravljanjeComponent } from './wishlist-upravljanje.component';

describe('WishlistUpravljanjeComponent', () => {
  let component: WishlistUpravljanjeComponent;
  let fixture: ComponentFixture<WishlistUpravljanjeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WishlistUpravljanjeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WishlistUpravljanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
