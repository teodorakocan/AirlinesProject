import { Component, OnInit, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../service/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DOCUMENT } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})

export class SigninComponent implements OnInit {
  user = {
    Email: '',
    Password: ''
  }
  fieldTextType: boolean;

  constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/home');
  }

  onSubmit(form: NgForm){
    this.service.userAuthentication(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('role', res.role);
        localStorage.setItem('email', form.value.Email);
        window.location.replace('/home');
      },
      err=>{
        if(err.status == 400)
        {
            this.toastr.error('Incorrect username or password.', 'Authentication failed.')
        }
        else if(err.status == 401)
        {
            this.toastr.error('Incorrect username or password.', 'Authentication failed.')
        }
        else{
          this.toastr.error('Error 500', 'Server failed.')
        }
      }
    )
  }

  /*LoginWithGoogle(socialPlatform : string){
    let socialPlatformProvider;
    if(socialPlatform == "facebook"){
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    }else if(socialPlatform == "google"){
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    } 
    
    this.authService.signIn(socialPlatformProvider).then(socialusers => {  
      console.log(socialusers);   

      this.service.externalLogin(socialusers).subscribe((res:any)=>{
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/home');
      });
   
      console.log(socialusers);  
    });  

  }*/

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
