import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountLogInComponent } from './login.component';

describe('AccountLogInComponent', () => {
  let component: AccountLogInComponent;
  let fixture: ComponentFixture<AccountLogInComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountLogInComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountLogInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
