import { Component, OnInit, Input } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import { Observable } from 'rxjs';
import { Airline } from 'src/app/models';
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
  state$;
  public airline_info: Airline;

  ngOnInit(){
    this.state$ = this.route.params.subscribe(params => {
      debugger
      this.name = params['name'];

      this.service.airlineInfo(this.name).subscribe(
        (airline: Airline) => {
          debugger
          this.airline_info = airline;
          });
      });
     
      
  }
}
