import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { HttpClientModule } from '@angular/common/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxBootstrapIconsModule } from 'ngx-bootstrap-icons';
import { personBoundingBox, creditCard } from "ngx-bootstrap-icons";

const icons = {
    personBoundingBox,
    creditCard
};

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { EventsComponent } from './pages/events/events.component';
import { EventTicketsComponent } from './pages/events/tickets/tickets.component';
import { AccountsComponent } from './accounts/accounts.component';
import { AccountTicketsComponent } from './account-tickets/account-tickets.component';
import { AccountLogInComponent } from './pages/account/login/login.component';
import { AccountSignInComponent } from './pages/account/signin/signin.component';
import { AccountInfoComponent } from './pages/account/info/info.component';
import { AccountSettingsComponent } from './pages/account/settings/settings.component';
import { AccountLogOutComponent } from './pages/account/logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    EventsComponent,
    EventTicketsComponent,
    AccountsComponent,
    AccountTicketsComponent,
    AccountLogInComponent,
    AccountSignInComponent,
    AccountLogOutComponent,
    AccountInfoComponent,
    AccountSettingsComponent,
  ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        ReactiveFormsModule,
        NgbModule,
        NgxBootstrapIconsModule.pick(icons),
        FormsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
