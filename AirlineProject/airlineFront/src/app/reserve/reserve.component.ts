import { Component, OnInit } from '@angular/core';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RentService, Branch, Vehicle } from 'src/app/models';

@Component({
  selector: 'app-reserve',
  templateUrl: './reserve.component.html',
  styleUrls: ['./reserve.component.css']
})
export class ReserveComponent implements OnInit {

  constructor(private route: ActivatedRoute, private toastr: ToastrService, public service: RentACarService, private router: Router) { }
  state$;
  public branchId:number;
  public vehicles: Vehicle[];
  minDate: Date;
  maxDate: Date;
  search = {
    Brand: '',
    NumberOfSeats: '',
    Price: '',
    DateRange: ''
  }
  

  ngOnInit(){
    this.minDate = new Date();
    this.maxDate = new Date();
    this.minDate.setDate(this.minDate.getDate());
    this.maxDate.setDate(this.minDate.getDate() + 366);
    this.state$ = this.route.params.subscribe(params => {
      this.branchId = params['branchId'];
    });

    this.service.branchVehicle(this.branchId).subscribe(
      (res:Vehicle[])=>{
        this.vehicles = res;
      },
      err=>{
        this.toastr.error('Error 500.','Serve failed.');
      }
    );
  }

  createImgPath(serverPath: string){
    return `http://localhost:50081/${serverPath}`;
  }

  SearchVehicle(){
    let startDate;
    let endDate;
    if(this.search.DateRange != "")
    {
      startDate = JSON.stringify(this.search.DateRange[0])
      startDate = startDate.slice(1,11)
      endDate = JSON.stringify(this.search.DateRange[1])
      endDate = endDate.slice(1,11)
    }
    else{
      endDate = "0001-01-01";
      startDate = "0001-01-01";
    }
    var searched={
      Brand: this.search.Brand,
      NumberOfSeats: this.search.NumberOfSeats,
      Price: this.search.Price,
      StartDate: startDate,
      EndDate: endDate
    }
    this.service.searchVehicle2(this.branchId, searched).subscribe(
      (res:Vehicle[])=>{
        this.vehicles = res;
      },
      err=>{
        this.toastr.error('Error 500.','Serve failed.');
      }
    ); 
  }

  ReserveVehicle(vehicle)
  {
    debugger
    let startDate;
    let endDate;
    if(this.search.DateRange != "")
    {
      startDate = JSON.stringify(this.search.DateRange[0])
      startDate = startDate.slice(1,11)
      endDate = JSON.stringify(this.search.DateRange[1])
      endDate = endDate.slice(1,11)
    }
    else{
      endDate = "0001-01-01";
      startDate = "0001-01-01";
    }

    var reserved={
      ReservationFrom: startDate,
      ReservationTo: endDate
    }
    debugger
    this.service.reserveVehicle(this.branchId, vehicle.id, reserved).subscribe(
      (res)=>
      {
        this.toastr.success('You reserved '+vehicle.name+'.Total price is '+res+'$','Reservation successful.');
      },
      err=>{
        this.toastr.error('Error 500.','Serve failed.');
      }
    );
  }
}
