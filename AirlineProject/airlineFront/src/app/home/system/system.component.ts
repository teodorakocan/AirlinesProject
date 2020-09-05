import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Admin, User, Discounts } from 'src/app/models';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { from } from 'rxjs';
import { SystemAdminService } from 'src/app/service/systemadmin.service';

@Component({
  selector: 'app-system',
  templateUrl: './system.component.html',
  styleUrls: ['./system.component.css']
})
export class SystemComponent implements OnInit {

  constructor(public systemservice: SystemAdminService, private toastr: ToastrService, private router: Router) { }

  public users: Object[];
  public companies: Object[];
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
  }

  SearchUsers(){
    var searched={
      Role: this.searchUser.Role,
      Email: this.searchUser.Email,
      Name: this.searchUser.Name,
      Surname: this.searchUser.Surname
    }
    this.systemservice.findUser(searched).subscribe(
      (res: Object[]) => {
        this.users = res;
      }, 
      err=>{
        this.toastr.error('Error 500.','Server failed.');
      }
    );
  }

  SearchCompany(form: NgForm)
  {
    
  }
}
