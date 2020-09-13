import { Component, OnInit } from '@angular/core';
import { AirlineService } from '../service/airline.service';
import { RentACarService } from '../service/rent_a_car.service'
import { Router } from '@angular/router';
import { RentService, Vehicle, Airline } from '../models';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  constructor(public airlineService: AirlineService, private toastr: ToastrService, private router: Router, public rentaService: RentACarService) {}
  
  public airlinesCompanies: Airline[];
  minDate: Date;
  maxDate: Date;
  public rentacarCompanies;
  public airlines: string[];
  public logged:boolean;
  public rentservices: string[];
  search = {
    Name: '',
    City: '',
    State: '',
    DateRange: ''
  }
  
  ngOnInit(){
    this.minDate = new Date();
    this.maxDate = new Date();
    this.minDate.setDate(this.minDate.getDate());
    this.maxDate.setDate(this.minDate.getDate() + 366);
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
      (companies) => {
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
    let startDate;
    let endDate;
    if(this.search.DateRange != "")
    {
      startDate = JSON.stringify(this.search.DateRange[0])
      debugger
      startDate = startDate.slice(1,11)
      endDate = JSON.stringify(this.search.DateRange[1])
      endDate = endDate.slice(1,11)
    }
    else{
      endDate = "0001-01-01";
      startDate = "0001-01-01";
    }
    var searched={
      Name: this.search.Name,
      City: this.search.City,
      StartDate: startDate,
      EndDate: endDate
    }
    debugger

    this.rentaService.searchService(searched).subscribe(
      (companies)=>{
        this.rentaService = companies;
      },
      err=>{
        this.toastr.error('Error 500.','Server failed.');
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
