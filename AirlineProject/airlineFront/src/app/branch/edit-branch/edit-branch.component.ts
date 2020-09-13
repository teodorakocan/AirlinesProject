import { Component, OnInit } from '@angular/core';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import {ActivatedRoute} from '@angular/router';
import {Router} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Branch } from 'src/app/models';
import { NgForm } from '@angular/forms';
import { Vehicle } from 'src/app/models';

@Component({
  selector: 'app-edit-branch',
  templateUrl: './edit-branch.component.html',
  styleUrls: ['./edit-branch.component.css']
})
export class EditBranchComponent implements OnInit {
  public branchId:number;
  public branch;
  vehicles: Vehicle[];
  exist1: boolean;
  state$;

  constructor(private route: ActivatedRoute, private toastr: ToastrService, public service: RentACarService, private router: Router) { }

  ngOnInit() {
    this.state$ = this.route.params.subscribe(params => {
      this.branchId = params['branchId'];

      this.service.getBranch(this.branchId).subscribe(
        (res)=>{
          this.branch = res;
        },
        err=>{
          this.toastr.error('Error 500','Server failed.');
        }
      )

      this.service.branchVehicle(this.branchId).subscribe(
        (res: Vehicle[])=>{
            if(res.length == 0)
            {
              this.exist1 == false;
            }
            else{
              this.exist1 = true;
              this.vehicles = res;
            }
        }
      )
    });
  }

  Edit(form: NgForm){
    var edit={
      Name: form.value.Name,
      Address: form.value.Address,
      City: form.value.City,
      State: form.value.State,
      NumberOfVehicle: form.value.NumberOfVehicle
    }
    this.service.editBranch(this.branch.id, edit).subscribe(
      (res)=>{
        this.toastr.success('You have successfully edit branch office.', 'Branch changed.')
      },
      err=>
      {
        if(err.status == 400)
        {
          this.toastr.error('Name is already taken','Choose another name.');
        }else{
          this.toastr.error('Error 500.', 'Server faild.')
        }
      }
    );
  }

  AddVehicle(){
    this.router.navigateByUrl('new-vehicle/' + this.branch.id);
  }

  DeleteBranch(){
    debugger
    this.service.deleteBranch(this.branch.id).subscribe(
      (res)=>{
        this.toastr.success('You have successfully deleted branch office.', 'Branch deleted.')
        this.router.navigateByUrl('rentService');
      },
      err=>{
        this.toastr.error('Error 500.', 'Server faild.')
      }
    );
  }

  DeleteVehicle(vehicleId){
    this.service.deleteVehicle(vehicleId).subscribe(
      (res)=>{
        this.toastr.success('Vehicle is successfully deleted', 'Deleted.');
        this.service.branchVehicle(this.branchId).subscribe(
          (res: Vehicle[])=>{
            this.vehicles = res;
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

  createImgPath(serverPath: string){
    return `http://localhost:50081/${serverPath}`;
  }
}
