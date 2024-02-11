import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBidDialogComponent } from './create-bid-dialog.component';

describe('CreateBidDialogComponent', () => {
  let component: CreateBidDialogComponent;
  let fixture: ComponentFixture<CreateBidDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateBidDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBidDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
