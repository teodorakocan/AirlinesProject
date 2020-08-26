import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FlightComponent } from './flight/flight.component';
import { SigninComponent } from './signin/signin.component';
import { HomeComponent } from './home/home.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { RegistartionComponent } from './signin/registartion/registartion.component';
import { AirlineComponent } from './home/profile/airline/airline.component';

const routes: Routes = [
  {
    path: "",
    redirectTo: "/home",
    pathMatch: 'full'
  },
  {
    path: "home",component: HomeComponent,
    children: [
      { path: 'flight', component: FlightComponent },
      { path: 'signin', component: SigninComponent},
      { path: 'registration', component: RegistartionComponent },
      { path: 'airline/:name', component:AirlineComponent}
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
