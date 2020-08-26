import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FlightComponent } from './flight/flight.component';
import { SigninComponent } from './signin/signin.component';
import { HomeComponent } from './home/home.component';
import { RegistartionComponent } from './signin/registartion/registartion.component';
import { UserService } from './service/user.service';
import { AuthGuard } from './auth/auth.guard';
import { AirlineComponent } from './home/profile/airline/airline.component';
import { AirlineService } from './service/airline.service';
 

@NgModule({
  declarations: [
    AppComponent,
    FlightComponent,
    SigninComponent,
    HomeComponent,
    RegistartionComponent,
    AirlineComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule,
    MDBBootstrapModule.forRoot()
  ],
  providers: [
    UserService,
    AirlineService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
