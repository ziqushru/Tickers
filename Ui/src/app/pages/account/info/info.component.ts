import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

class UserDto {
    name: string | null;
    surname: string | null;
    email: string | null;
    password: string | null;

    constructor(name: string, surname: string, email: string, password: string) {
        this.name = name;
        this.surname = surname;
        this.email = email;
        this.password = password;
    }
}

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css'],
})

@Injectable()
export class AccountInfoComponent implements OnInit {
    name: FormControl;
    surname: FormControl;
    email: FormControl;
    password: FormControl;

    user: UserDto;

    constructor(private http_client: HttpClient) {
       

        this.name = new FormControl('');
        this.surname = new FormControl('');
        this.email = new FormControl('');
        this.password = new FormControl('');

        this.user = new UserDto('', '', '', '');
    }

    ngOnInit(): void {
    }

    save(): void {
        this.user.name = this.name.value;
        this.user.surname = this.surname.value;
        this.user.email = this.email.value;
        this.user.password = this.password.value;
    }
}
