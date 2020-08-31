import { Component, OnInit } from '@angular/core';
import { AirlineService } from './service/airline.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Fly-Buy';
  public airlines: string[];
  selected; 
  open;
  public logged:boolean;
  public role: string;

  constructor(public service: AirlineService, private router: Router) { }
  
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

    this.service.airlineNames().subscribe(
    (names: string[]) => {
        //debugger
      this.airlines = names;
    });
  }

  onChange(airline: string){
    this.router.navigateByUrl('airline/' + airline);
  }
  
  SignOut(){
      localStorage.removeItem('token');
      localStorage.removeItem('email');
      localStorage.removeItem('role');
      window.location.replace('/signin');
  }
}
