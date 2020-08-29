import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { ToastrService } from 'ngx-toastr';
import { Admin } from 'src/app/models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService, private router: Router) { }
  public user;
  fieldTextType: boolean;
  public admins: Admin[];

  ngOnInit() {
    this.service.formModel.reset();
    var email = localStorage.getItem('email')
    this.service.getUserinfo(email).subscribe(
      (res)=>{
          this.user = res;
      },
      err=>{
          this.toastr.error('User does not exist.', 'Not found.')
      }
    )

    this.service.allAdmins().subscribe(
      (res: Admin[])=>{
        debugger
          this.admins = res;
      },
      err=>{
          this.toastr.error('There is no registered admins.', 'Not found.')
      }
    )
  }

  onSubmit(){
    debugger
    this.service.registerAdmin().subscribe(
      (res: any) => {
        debugger
        this.service.formModel.reset();
        this.toastr.success('Admin is successfully made', 'Registration successful.');
      },
      err=>{
        if(err.status == 400)
        {
          this.toastr.error('Username is already taken','Registration failed.');
        }
        else{
          this.toastr.error('Error 500','Server failed.');
        }
      }
    )
  }

  DeleteAccount(){
    this.service.deleteAccount(localStorage.getItem('email')).subscribe(
      (res)=>{
        this.toastr.success('Account deleted. Bye.', 'Successfuly deleted.')
      }
    )
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
