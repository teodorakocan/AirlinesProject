import { Component, OnInit } from '@angular/core';
import { Vehicle } from 'src/app/models';
import { HttpClient } from '@angular/common/http';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import {ActivatedRoute} from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-new-vehicle',
  templateUrl: './new-vehicle.component.html',
  styleUrls: ['./new-vehicle.component.css']
})
export class NewVehicleComponent implements OnInit {
  public isCreate: boolean;
  public vehicle: Vehicle;
  public Brand: string;
  public NumberOfSeats: number;
  public Class: string;
  public Price: number;
  public response: {dbPath: ''};
  public branchId:number;
  public vehicles: Vehicle[];
  state$;

  constructor(private http: HttpClient, private toastr: ToastrService, public service: RentACarService,private route: ActivatedRoute) { }

  ngOnInit() {
    this.isCreate = true;
    this.state$ = this.route.params.subscribe(params => {
      this.branchId = params['branchId'];
    });
  }

  onCreate(){
    this.vehicle = {
      Brand: this.Brand,
      NumberOfSeats: this.NumberOfSeats,
      Class: this.Class,
      Price:this.Price,
      Image: this.response.dbPath
    }

    this.service.addVehicle(this.branchId, this.vehicle).subscribe(res => {
      this.service.branchVehicle(this.branchId).subscribe(
        (res: Vehicle[])=>{
            this.vehicles = res;
        },
        err=>{
          if(err.status == 400)
          {
            this.toastr.error('You cannot add new vehicle. Change number of vehicle in branch office.','Adding failed.');
          }
          else{
            this.toastr.error('Error 500','Server failed.');
          }
        }
      );
      this.isCreate = false;
    });
  }

  returnToCreate(){
    this.isCreate = true;
    this.Brand =  '';
    this.NumberOfSeats = 0;
    this.Class = '';
    this.Price = 0;
  }

  uploadFinished(event){
    this.response = event;
  }

  createImgPath(serverPath: string){
    return `http://localhost:50081/${serverPath}`;
  }
}
