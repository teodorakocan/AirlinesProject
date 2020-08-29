import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './signin/signin.component';
import { HomeComponent } from './home/home.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { RegistrationComponent } from './signin/registration/registration.component';
import { AirlineComponent } from './home/profile/airline/airline.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { AppComponent } from './app.component';
import { SettingsComponent } from './home/settings/settings.component';

const routes: Routes = [
  {
    path: "",
    redirectTo: "/home",
    pathMatch: 'full'
  },
  { path: 'home',component: HomeComponent },
  { path: 'signin', component: SigninComponent},
  { path: 'registration', component: RegistrationComponent },
  { path: 'airline/:name', component:AirlineComponent},
  { path: 'confirmation/:email', component: ConfirmationComponent},
  { path: 'settings', component: SettingsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
