import { Component, OnInit } from '@angular/core';
import { RentACarComponent } from 'src/app/home/profile/rent-a-car/rent-a-car.component';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import {ActivatedRoute} from '@angular/router';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-new-branch',
  templateUrl: './new-branch.component.html',
  styleUrls: ['./new-branch.component.css']
})
export class NewBranchComponent implements OnInit {

  public serviceId:number;
  state$;
  constructor(private route: ActivatedRoute, private toastr: ToastrService, public service: RentACarService, private router: Router) { }

  ngOnInit() {
    this.state$ = this.route.params.subscribe(params => {
      this.serviceId = params['serviceId'];
    });
  }

  AddBranch(){
    this.service.addBranch(this.serviceId).subscribe(
      (res: any) => {
        this.service.formModel1.reset();
        this.toastr.success('Branch office is successfully added', 'Adding successful.');
        this.router.navigateByUrl('rentService');
      },
      err=>{
        if(err.status == 404)
        {
          this.toastr.error('Name for branch is taken. Choose another one.','Adding failed.');
        }
        else{
          this.toastr.error('Error 500','Server failed.');
        }
      }
    )
  }
}
