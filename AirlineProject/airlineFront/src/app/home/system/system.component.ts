import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Admin, User, Discounts } from 'src/app/models';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { from } from 'rxjs';
import { SystemAdminService } from 'src/app/service/systemadmin.service';
import { RentACarService } from 'src/app/service/rent_a_car.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-system',
  templateUrl: './system.component.html',
  styleUrls: ['./system.component.css']
})
export class SystemComponent implements OnInit {

  constructor(public systemservice: SystemAdminService, private toastr: ToastrService, private router: Router, 
    public rentacarservice: RentACarService, public service: UserService) { }

  public users: Object[];
  public companies: Object[];
  public reservations: Object[];
  searchUser ={
    Role: '',
    Name: '',
    Surname: '',
    Email: ''
  };
  searchCompany ={
    Type: '',
    Name: '',
    City: '',
    State: ''
  }

  ngOnInit() {
    this.systemservice.allUsers().subscribe(
      (res:Object[])=>{
        this.users = res;
      },
      err=>
      {
        if(err.status == 400)
        {
          this.toastr.error('There are no registered users','Failed to load users.');
        }
        else{
          this.toastr.error('Error 500','Server failed.');
        }
      }
    );

    this.systemservice.allCompanies().subscribe(
      (res:Object[])=>{
        this.companies = res;
      },
      err=>{
        if(err.status == 400)
        {
          this.toastr.error('There are no registered companies','Failed to load companies.');
        }
        else{
          this.toastr.error('Error 500','Server failed.');
        }
      }
    );

    this.systemservice.allReservations().subscribe(
      (res:Object[])=>{
        this.reservations = res;
      },
      err=>{
        if(err.status == 400)
        {
          this.toastr.error('There are no reservations','Failed to load reservations.');
        }
        else{
          this.toastr.error('Error 500','Server failed.');
        }
      }
    );
  }

  DeleteCompany(id){
    this.rentacarservice.deleteService(id).subscribe(
      (res)=>
      {
        this.toastr.success('Company is deleted','Successful deleted.');
        this.systemservice.allCompanies().subscribe(
          (res:Object[])=>{
            this.companies = res;
          },
          err=>{
            if(err.status == 400)
            {
              this.toastr.error('There are no registered companies','Failed to load companies.');
            }
            else{
              this.toastr.error('Error 500','Server failed.');
            }
          }
        );
      }
    );
  }

  DeleteUser(userEmail){
    this.service.deleteAccount(userEmail).subscribe(
      (res)=>{
        this.toastr.success('User is successfully deleted','Success.');
        this.systemservice.allUsers().subscribe(
          (res:Object[])=>{
            this.users = res;
          },
          err=>
          {
            if(err.status == 400)
            {
              this.toastr.error('There are no registered users','Failed to load users.');
            }
            else{
              this.toastr.error('Error 500','Server failed.');
            }
          }
        );
      },
      err=>
      {
        this.toastr.error('Error 500','Server failed.');
      }
    );
  }
}