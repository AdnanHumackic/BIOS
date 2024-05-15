import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatOkDialogComponent } from './mat-ok-dialog.component';

describe('MatOkDialogComponent', () => {
  let component: MatOkDialogComponent;
  let fixture: ComponentFixture<MatOkDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatOkDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MatOkDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
