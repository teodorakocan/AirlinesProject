import { Component, OnInit } from '@angular/core';
import { AirlineService } from './service/airline.service';
import {Router} from '@angular/router';
import { RentACarService } from './service/rent_a_car.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Fly-Buy';
  public airlines: string[];
  public rentservices: string[];
  selected; 
  open;
  public logged:boolean;
  public role: string;

  constructor(public service: AirlineService, public rentacarservice: RentACarService, private router: Router) { }
  
  ngOnInit(){
    if(localStorage.getItem("token") != null)
    {
      this.role = localStorage.getItem('role');
      this.logged = true;
    }
    else
    {
      this.logged = false;
    }
  }

  /*onChangeAirline(airline: string){
    this.router.navigateByUrl('airline/' + airline);
  }

  onChangeRent(rentservice: string){
    this.router.navigateByUrl('services/' + rentservice);
  }*/
  
  SignOut(){
      localStorage.removeItem('token');
      localStorage.removeItem('email');
      localStorage.removeItem('role');
      window.location.replace('/signin');
  }
}
