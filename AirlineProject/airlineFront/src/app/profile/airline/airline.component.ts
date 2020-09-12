import { Component, OnInit } from '@angular/core';
import { AirlineService } from 'src/app/service/airline.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Airline, Destination, Flight } from 'src/app/models';

@Component({
  selector: 'app-airline',
  templateUrl: './airline.component.html',
  styleUrls: ['./airline.component.css']
})
export class AirlineComponent implements OnInit {

  constructor(private route: ActivatedRoute, private toastr: ToastrService, public service: AirlineService, private router: Router) { }
  public name:string;
  airline;
  state$;
  public destinations: Destination[];
  public flights: Flight[];
  search = {
    Destination: '',
    FlightNumber: '',
    NumberTransfer: '',
    DestinationTransfer: '',
    TravelLenght: '',
    Price: '',
    StartDate: '',
    EndDate: ''
  }

  ngOnInit(){
    this.state$ = this.route.params.subscribe(params => {
      this.name = params['name'];
    });

    this.service.getAirlineInfo(this.name).subscribe(
      (res: Airline)=>{
        this.airline = res;
      },
      err=>{
        if(err.status == 404)
        {
          this.toastr.error('Airline company is not existing.','Failed.');
        }
        else
        {
          this.toastr.error('Error 500.','Server failed.');
        }
      }
    );
  }

  SearchFlight(){}
}
