import { Component, OnInit, Input } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import { Observable } from 'rxjs';
import { Destination } from 'src/app/models';
import { UserService } from 'src/app/service/user.service';
import { AirlineService } from 'src/app/service/airline.service';

@Component({
  selector: 'app-airline',
  templateUrl: './airline.component.html',
  styleUrls: ['./airline.component.css']
})
export class AirlineComponent implements OnInit {

  constructor(private route: ActivatedRoute, public service: AirlineService) { }
  @Input() selectedAirline: string;

  public name:string;
  public destinations: Destination[];
  state$;
  airline_info;

  ngOnInit(){
    this.state$ = this.route.params.subscribe(params => {
      this.name = params['name'];

      this.service.airlineInfo(this.name).subscribe(
        (airline) => {
          //debugger
          this.airline_info = airline;
          });
      });

      this.service.getAirlineDestinations(this.name).subscribe(
        (destinations:Destination[])=>{
            this.destinations = destinations;
        }
      )
  }
}
