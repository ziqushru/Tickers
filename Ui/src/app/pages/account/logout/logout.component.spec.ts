import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountLogOutComponent } from './logout.component';

describe('AccountLogOutComponent', () => {
  let component: AccountLogOutComponent;
  let fixture: ComponentFixture<AccountLogOutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountLogOutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountLogOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
