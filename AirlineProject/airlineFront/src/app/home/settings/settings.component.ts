import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { ToastrService } from 'ngx-toastr';
import { Admin, User, Discounts } from 'src/app/models';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { from } from 'rxjs';
import { SystemAdminService } from 'src/app/service/systemadmin.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(public service: UserService, public systemservice: SystemAdminService, private toastr: ToastrService, private router: Router) { }
  public user;
  public email;
  public role;
  fieldTextType: boolean;
  public admins: Admin[];
  public users: User[];
  public discounts: Discounts[];
  public reservations:Object[];
  public oldReservations: Object[];
  discount={
    Points: 0,
    Discounts: 0
  }

  ngOnInit() {
    this.service.formModel1.reset();
    this.email = localStorage.getItem('email');
    this.role = localStorage.getItem('role');
    this.service.getUserinfo(this.email).subscribe(
      (res)=>{
          this.user = res;
      },
      err=>{
          this.toastr.error('User does not exist.', 'Not found.')
      }
    )

    if(this.role == 'Admin')
    {
      this.systemservice.allDiscounts().subscribe(
        (res: Discounts[])=>{
            this.discounts = res;
        },
        err=>{
            this.toastr.error('There is no registered admins.', 'Not found.')
        }
      )
    }
    if(this.role == "User")
    {
      this.service.Reservations(this.email).subscribe(
        (res:Object[])=>{
          this.reservations = res;
        }
      );
        this.service.OldReservations(this.email).subscribe(
          (res:Object[])=>{
            this.oldReservations = res;
          }
        );
    }
  }

  onSubmit(){
    this.systemservice.registerAdmin().subscribe(
      (res: any) => {
        this.service.formModel1.reset();
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
        localStorage.removeItem('role');
        localStorage.removeItem('email');
        localStorage.removeItem('token');
        this.toastr.success('Account deleted. Bye.', 'Successfuly deleted.')
      }
    )
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

  EditDiscount(form: NgForm, id){
    var disount={
      Points: form.value.Points,
      Discount: form.value.Discount
    }
    this.systemservice.editDiscount(disount, id).subscribe(
      (res)=>{
        this.toastr.success('You have successfully changed discount.', 'Discount changed.')
        this.systemservice.allDiscounts().subscribe(
          (res: Discounts[])=>{
              this.discounts = res;
          },
          err=>{
              this.toastr.error('There is no registered admins.', 'Not found.')
          }
        )
      },
      err=>
      {
        this.toastr.error('Error 500.', 'Server faild.')
      }
    );
  }

  addDiscount(){
    this.systemservice.addDiscount().subscribe(
      (res: any) => {
        this.systemservice.formModel3.reset();
        this.toastr.success('Discount is successfully added', 'Adding successful.');
        this.systemservice.allDiscounts().subscribe(
          (res: Discounts[])=>{
              this.discounts = res;
          },
          err=>{
              this.toastr.error('There is no registered admins.', 'Not found.')
          }
        )
      },
      err=>{
          this.toastr.error('Error 500','Server failed.');
      }
    )
  }
}
