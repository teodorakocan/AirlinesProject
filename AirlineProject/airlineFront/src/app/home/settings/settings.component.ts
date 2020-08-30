import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { ToastrService } from 'ngx-toastr';
import { Admin, User } from 'src/app/models';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { from } from 'rxjs';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService, private router: Router) { }
  public user;
  public email;
  public role;
  fieldTextType: boolean;
  public admins: Admin[];
  public users: User[];
  searchedUser = {
    Name: '',
    Surname: '',
    ID: 0
  }

  ngOnInit() {
    this.service.formModel.reset();
    this.email = localStorage.getItem('email');
    this.role = localStorage.getItem('role');
    this.service.getUserinfo(this.email).subscribe(
      (res)=>{
        debugger
          this.user = res;
      },
      err=>{
          this.toastr.error('User does not exist.', 'Not found.')
      }
    )

    if(this.role == 'Admin')
    {
      this.service.allAdmins().subscribe(
        (res: Admin[])=>{
            this.admins = res;
        },
        err=>{
            this.toastr.error('There is no registered admins.', 'Not found.')
        }
      )
    }
  }

  onSubmit(){
    debugger
    this.service.registerAdmin().subscribe(
      (res: any) => {
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

  Search(form: NgForm){
    var id = form.value.ID;
    var name = form.value.Name;
    var surname = form.value.Surname;
    this.service.findAdmin(id, name, surname).subscribe(
      (res: Admin[])=>{
        this.admins = res;
      },
      err=>{
          this.toastr.error('Admin is not existing.', 'Not found.')
      }
    );
  }

  SearchUser(form: NgForm){
    var id = form.value.ID;
    var name = form.value.Name;
    var surname = form.value.Surname;
    this.service.findUser(id, name, surname).subscribe(
      (res: User[])=>{
        this.users = res;
      },
      err=>{
          this.toastr.error('User is not existing.', 'Not found.')
      }
    );
  }

  Edit(form: NgForm){
      var edit={
        Name: form.value.Name,
        Surname: form.value.Surname,
        City: form.value.City,
        PhoneNumber: form.value.PhoneNumber
      }
      this.service.editProfil(this.email, edit).subscribe(
        (res)=>{
          this.toastr.success('You have successfully changed your profile.', 'Profil changed.')
        },
        err=>
        {
          this.toastr.error('Error 500.', 'Server faild.')
        }
      );
  }

  changePassword(){
    this.service.changePassword(this.email).subscribe(
      (res: any) => {
        this.toastr.success('Password is changed');
      },
      err=>{
        this.toastr.error('Error 500','Server failed.');
      }
    )
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
