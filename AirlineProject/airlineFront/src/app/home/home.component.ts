import { Component, OnInit } from '@angular/core';
import { AirlineService } from '../service/airline.service';
import { Router } from '@angular/router';
import { Flight } from '../models';
import { RentACarService } from '../service/rent_a_car.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  constructor(public service: AirlineService, private router: Router, public rentaService: RentACarService) { }
  public flights: Flight[];
  public airlines: string[];
  public logged:boolean;

  ngOnInit(){
    //debugger
    if(localStorage.getItem("token") != null)
    { 
      this.logged = true;
    }
    else
    {
      this.logged = false;
    }

    this.service.getAllFlights().subscribe(
      (allFlights:Flight[])=>{
        //debugger
          this.flights = allFlights;
      }
    );

    this.service.airlineNames().subscribe(
      (names: string[]) => {
          //debugger
        this.airlines = names;
      });
    /*this.rentaService.getCities().subscribe(
      (names: string[]) => {
        //debugger
        this.airlines = names;
        }
    );*/
  }
}
