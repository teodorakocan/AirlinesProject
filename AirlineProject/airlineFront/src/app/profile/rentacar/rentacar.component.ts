import { Component, OnInit } from '@angular/core';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import {ActivatedRoute} from '@angular/router';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RentService, Branch, Vehicle } from 'src/app/models';

@Component({
  selector: 'app-rentacar',
  templateUrl: './rentacar.component.html',
  styleUrls: ['./rentacar.component.css']
})
export class RentacarComponent implements OnInit {

  constructor(private route: ActivatedRoute, private toastr: ToastrService, public service: RentACarService, private router: Router) { }
  public name:string;
  rentservice;
  public serviceId:number;
  public branches: Branch[];
  public vehicles: Vehicle[];
  state$;
  search = {
    Brand: '',
    NumberOfSeats: '',
    Class: '',
    Price: ''
  }

  ngOnInit() {
    this.state$ = this.route.params.subscribe(params => {
      this.name = params['name'];
    });
    
    this.service.getServiceInfo(this.name).subscribe(
      (res)=>{
        this.rentservice = res;
        this.serviceId = this.rentservice.id;
        this.service.allBranches(this.rentservice.id).subscribe(
          (res: Branch[])=>{
            this.branches = res;
          },
          err=>{
            this.toastr.error('Error 500.','Server failed.');
          }
        );
        this.service.serviceVehicle(this.rentservice.id).subscribe(
          (res: Vehicle[])=>{
            this.vehicles = res;
          },
          err=>{
            this.toastr.error('Error 500.','Server failed.');
          }
        );
      },
      err=>{
        if(err.status == 404)
        {
          this.toastr.error('Rent-a-car service is not existing.','Failed.');
        }
        else
        {
          this.toastr.error('Error 500.','Server failed.');
        }
      }
    );
  }

  SearchVehicle(){
    var searched={
      Brand: this.search.Brand,
      NumberOfSeats: this.search.NumberOfSeats,
      Class: this.search.Class,
      Price: this.search.Price
    }

    this.service.searchVehicle(this.serviceId,searched).subscribe(
      (res: Vehicle[])=>{
        this.vehicles = res;
      },
      err=>{
        this.toastr.error('Error 500.','Server failed.');
      }
    );
  }

  createImgPath(serverPath: string){
    return `http://localhost:50081/${serverPath}`;
  }
}
