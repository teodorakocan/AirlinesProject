import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService, private router: Router) { }
  fieldTextType: boolean;

  ngOnInit() {
    this.service.formModel.reset();
  }

  onSubmit(){
    this.service.register().subscribe(
      (res: any) => {
        this.service.formModel.reset();
        this.toastr.success('We sand you email to confirm your registration', 'Registration successful.');
        this.router.navigateByUrl('/home');
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

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
