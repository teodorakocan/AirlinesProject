import { Component, OnInit } from '@angular/core';
import { AirlineService } from '../service/airline.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public airlines: string[];
  selected; 
  open;

  constructor(public service: AirlineService, private router: Router) { }

  ngOnInit(){
    this.service.airlineNames().subscribe(
      (names: string[]) => {
        //debugger
        this.airlines = names;
        });
  }

  onChange(airline: string){
    this.router.navigateByUrl('/home/airline/' + airline);
  }
}
