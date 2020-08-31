import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './signin/signin.component';
import { HomeComponent } from './home/home.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { RegistrationComponent } from './signin/registration/registration.component';
import { AirlineComponent } from './home/profile/airline/airline.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { AppComponent } from './app.component';
import { AuthGuard } from './auth/auth.guard';
import { SettingsComponent } from './home/settings/settings.component';
import { AdminConfirmationComponent } from './admin-confirmation/admin-confirmation.component';
import { RentACarComponent } from './home/profile/rent-a-car/rent-a-car.component';
import { RentServiceComponent } from './rent-service/rent-service.component';
import { RoleServiceAdminGuard } from './auth/auth.roleServiceAdmin';
import { NewBranchComponent } from './branch/new-branch/new-branch.component';
import { EditBranchComponent } from './branch/edit-branch/edit-branch.component';
import { NewVehicleComponent } from './vehicle/new-vehicle/new-vehicle.component';
import { EditVehicleComponent } from './vehicle/edit-vehicle/edit-vehicle.component';

const routes: Routes = [
  {
    path: "",
    redirectTo: "/home",
    pathMatch: 'full',
  },
  { path: 'home',component: HomeComponent },
  { path: 'signin', component: SigninComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'airline/:name', component:AirlineComponent },
  { path: 'confirmation/:email', component: ConfirmationComponent },
  { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
  { path: 'adminConfirmation/:email', component: AdminConfirmationComponent },
  { path: 'rentService', component: RentServiceComponent, canActivate: [RoleServiceAdminGuard]},
  { path: 'new-branch/:serviceId', component:NewBranchComponent, canActivate: [RoleServiceAdminGuard]},
  { path: 'edit-branch/:branchId', component:EditBranchComponent, canActivate: [RoleServiceAdminGuard]},
  { path: 'new-vehicle/:branchId', component:NewVehicleComponent, canActivate: [RoleServiceAdminGuard]},
  { path: 'edit-vehicle/:vehicleId', component:EditVehicleComponent, canActivate: [RoleServiceAdminGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
