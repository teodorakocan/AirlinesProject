import { Component, OnInit } from '@angular/core';
import { AirlineService } from '../service/airline.service';
import { RentACarService } from '../service/rent_a_car.service'
import { Router } from '@angular/router';
import { RentService, Vehicle, Airline } from '../models';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  constructor(public airlineService: AirlineService, private toastr: ToastrService, private router: Router, public rentaService: RentACarService) {
   }
  
  public airlinesCompanies: Airline[];
  public rentacarCompanies: RentService[];
  public airlines: string[];
  public logged:boolean;
  public rentservices: string[];
  search = {
    Name: '',
    City: '',
    State: ''
  }
  
  ngOnInit(){
    if(localStorage.getItem("token") != null)
    { 
      this.logged = true;
    }
    else
    {
      this.logged = false;
    }

    this.airlineService.airlineNames().subscribe(
      (names: string[]) => {
          this.airlines = names;
      });
    
    this.airlineService.allAirlines().subscribe(
      (companies: Airline[]) => {
          this.airlinesCompanies = companies;
      });

    this.rentaService.allServices().subscribe(
      (companies: RentService[]) => {
          this.rentacarCompanies = companies;
      });

    this.rentaService.serviceNames().subscribe(
      (names: string[]) => {
        this.rentservices = names;
      });
  }

  createImgPath(serverPath: string){
    return `http://localhost:50081/${serverPath}`;
  }

  Reserve(vehicle: Vehicle)
  {
    if(localStorage.getItem('token') == null)
    {
      this.toastr.error('You have to be register to reserve vehicle','Reservation denied.');
    }
    else{
      if(localStorage.getItem('role') != "User")
      {
        this.toastr.error('With admin role you cannot make reservatio.','Reservation denied.');
      }
      //rezervisi vozilo
    }
  }

  SearchAirlines(form: NgForm){

    
  }

  SearchServices(){
    var searched={
      Name: this.search.Name,
      City: this.search.City,
      State: this.search.State
    }
    this.rentaService.findService(searched).subscribe(
      (companies: RentService[]) => {
        debugger
        if(companies.length > 1)
        {
          this.toastr.error('Rent-a-car service that you search is not existing.','Searching failed.');
        }
        this.rentacarCompanies = companies;
      }, 
      err=>{
        if(err.status == 404)
        {
          this.toastr.error('Rent-a-car service that you search is not existing.','Searching failed.');
        }
        else
        {
          this.toastr.error('Error 500.','Server failed.');
        }
      }
    );
  }

  ProfileService(rentservice: string){
    this.router.navigateByUrl('rentacar/' + rentservice);
  }
  
  ProfileAirline(airline: string){
    this.router.navigateByUrl('airline/' + airline);
  }
}
