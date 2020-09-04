import { Component, OnInit } from '@angular/core';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import {ActivatedRoute} from '@angular/router';
import {Router} from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-vehicle',
  templateUrl: './edit-vehicle.component.html',
  styleUrls: ['./edit-vehicle.component.css']
})
export class EditVehicleComponent implements OnInit {
  public vehicleId:number;
  public vehicle;
  public response: {dbPath: ''};
  public uploded: boolean;
  state$;

  constructor(private route: ActivatedRoute, private toastr: ToastrService, public service: RentACarService, private router: Router) { }

  ngOnInit(){
    this.uploded = false;
    this.state$ = this.route.params.subscribe(params => {
      this.vehicleId = params['vehicleId'];

      this.service.getVehicle(this.vehicleId).subscribe(
        (res)=>{
          debugger
          this.vehicle = res;
        },
        err=>{
          this.toastr.error('Error 500','Server failed.');
        }
      )
    });
  }

  onCreate(){
    if(!this.uploded)
    {
      this.vehicle = {
        Brand: this.vehicle.brand,
        NumberOfSeats: this.vehicle.numberOfSeats,
        Class: this.vehicle.class,
        Price:this.vehicle.price,
        Image: this.vehicle.image
      }
    }
    else{
      this.vehicle = {
        Brand: this.vehicle.brand,
        NumberOfSeats: this.vehicle.numberOfSeats,
        Class: this.vehicle.class,
        Price:this.vehicle.price,
        Image: this.response.dbPath
      }
    }

    this.service.editVehicle(this.vehicleId, this.vehicle).subscribe(
        (res)=>{
          this.toastr.success('Vehicle is successfully changed', 'Changed.');
          this.router.navigateByUrl('rentService');
        },
        err=>{
          this.toastr.error('Error 500','Server failed.');
        }
    );
  }

  uploadFinished(event){
    this.uploded = true;
    this.response = event;
  }

  createImgPath(serverPath: string){
    return `http://localhost:50081/${serverPath}`;
  }
}
