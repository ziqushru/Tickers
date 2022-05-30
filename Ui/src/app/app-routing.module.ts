import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

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

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'events', component: EventsComponent },
  { path: 'events/:id/tickets', component: EventTicketsComponent },
  { path: 'accounts', component: AccountsComponent },
  { path: 'accounts/tickets', component: AccountTicketsComponent },
  { path: 'account/login', component: AccountLogInComponent },
  { path: 'account/signin', component: AccountSignInComponent },
  { path: 'account/info', component: AccountInfoComponent },
  { path: 'account/settings', component: AccountSettingsComponent },
  { path: 'account/logout', component:  AccountLogOutComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
