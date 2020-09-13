import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MatTabsModule } from '@angular/material/tabs';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SigninComponent } from './signin/signin.component';
import { HomeComponent } from './home/home.component';
import { UserService } from './service/user.service';
import { AuthGuard } from './auth/auth.guard';
import { AirlineService } from './service/airline.service';
import { RentACarService } from './service/rent_a_car.service';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { CookieService } from 'ngx-cookie-service';
import { TokenInterceptor } from './auth/TokenInterceptor';
import { SettingsComponent } from './home/settings/settings.component';
import { RegistrationComponent } from './signin/registration/registration.component';
import { AdminConfirmationComponent } from './admin-confirmation/admin-confirmation.component';
import { UploadComponent } from './upload/upload.component';
import { NewBranchComponent } from './branch/new-branch/new-branch.component';
import { EditBranchComponent } from './branch/edit-branch/edit-branch.component';
import { NewVehicleComponent } from './vehicle/new-vehicle/new-vehicle.component';
import { EditVehicleComponent } from './vehicle/edit-vehicle/edit-vehicle.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RentacarComponent } from './profile/rentacar/rentacar.component';
import { AirlineComponent } from './profile/airline/airline.component';
import { SystemAdminService } from './service/systemadmin.service';
import { SystemComponent } from './home/system/system.component';
import { RentServiceComponent } from './home/rent-service/rent-service.component';

@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    HomeComponent,
    RegistrationComponent,
    ConfirmationComponent,
    SettingsComponent,
    RegistrationComponent,
    AdminConfirmationComponent,
    UploadComponent,
    NewBranchComponent,
    EditBranchComponent,
    NewVehicleComponent,
    EditVehicleComponent,
    RentacarComponent,
    AirlineComponent,
    SystemComponent,
    RentServiceComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTabsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule,
    MDBBootstrapModule.forRoot(),
    BsDatepickerModule.forRoot(),
    NgbModule
  ],
  providers: [
    CookieService,
    SystemAdminService,
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    AirlineService,
    RentACarService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
