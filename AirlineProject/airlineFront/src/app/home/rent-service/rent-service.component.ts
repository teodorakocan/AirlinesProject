import { Component, OnInit } from '@angular/core';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Branch, Vehicle } from 'src/app/models';

@Component({
  selector: 'app-rent-service',
  templateUrl: './rent-service.component.html',
  styleUrls: ['./rent-service.component.css']
})
export class RentServiceComponent implements OnInit {

  constructor(public service: RentACarService, private toastr: ToastrService, private router: Router) { }
  public email = localStorage.getItem('email');
  public myService;
  public branches: Branch[];
  public vehicles: Vehicle[];
  public exist1: boolean;
  public exist2: boolean;
  public exist3: boolean;
  public serviceId:number;

  ngOnInit() {
    this.service.getMyService(this.email).subscribe(
      (res)=>{
        if(res == null)
        {
          this.exist1 = false;
        }
        else{
          this.exist1 = true;
          this.myService = res;
          this.serviceId = this.myService.id;
          this.service.allBranches(this.serviceId).subscribe(
            (res: Branch[])=>{
                if(res.length == 0)
                {
                    this.exist2 == false;
                }
                else{
                  this.branches = res;
                  this.exist2 = true;
                  this.service.serviceVehicle(this.serviceId).subscribe(
                    (res: Vehicle[])=>{
                        if(res.length == 0)
                        {
                          this.exist3 == false;
                        }
                        else{
                          this.exist3 = true;
                          this.vehicles = res;
                        }
                    }
                  )
                }
              }
            );
        }
      }
    )
  }

  AddService(){
    this.service.addService().subscribe(
      (res: any) => {
        this.service.formModel.reset();
        this.toastr.success('Rent-a-car service is successfully made', 'Registration successful.');
        this.exist1 = true;
        this.ngOnInit();
      },
      err=>{
        if(err.status == 404)
        {
          this.toastr.error('Name for rent-a-car service is taken. Choose another one.','Registration failed.');
        }
        else if(err.status == 400)
        {
          this.toastr.error('You already have rent-a-car service.','Adding failed.');
        }
        else{
          this.toastr.error('Error 500','Server failed.');
        }
      }
    )
  }

  CreateBranch(){
    this.router.navigateByUrl('new-branch/' + this.serviceId);
  }

  EditBranch(branchId){
    //debugger
    this.router.navigateByUrl('edit-branch/' + branchId);
  }

  DeleteService(){
    this.service.deleteService(this.serviceId).subscribe(
        (res)=>{
          this.toastr.success('Rent-a-car service is successfully deleted', 'Deleted.');
          this.exist1 = false;
          this.ngOnInit();
        },
        err=>{
          this.toastr.error('Error 500','Server failed.');
        }
    )
  }

  createImgPath(serverPath: string){
    return `http://localhost:50081/${serverPath}`;
  }

  DeleteVehicle(vehicleId){
    this.service.deleteVehicle(vehicleId).subscribe(
      (res)=>{
        this.toastr.success('Vehicle is successfully deleted', 'Deleted.');
        this.service.serviceVehicle(this.serviceId).subscribe(
          (res: Vehicle[])=>{
            this.vehicles = res;
            this.ngOnInit();
          }
        );
      },
      err=>{
        if(err.status == 400)
        {
          this.toastr.error('This vehicle is reserved.','Delete failed.');
        }
        else
        {
          this.toastr.error('Error 500','Server failed.');
        }
      }
    )
  }

  EditVehicle(vehicleId){
    this.router.navigateByUrl('edit-vehicle/' + vehicleId);
  }

  EditService(form: NgForm)
  {
    var edit={
      Name: form.value.Name,
      Promo_Description: form.value.Promo_Description,
      Address: form.value.Address
    }
    this.service.editService(this.serviceId, edit).subscribe(
      (res)=>{
        this.toastr.success('You have successfully changed rent-a-car service profile.', 'Profil changed.')
      },
      err=>
      {
        this.toastr.error('Error 500.', 'Server faild.')
      }
    );
  }
}
