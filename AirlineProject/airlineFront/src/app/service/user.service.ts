import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class UserService{

    constructor(private _http: HttpClient, private fb: FormBuilder){}

    private _baseUrl = environment.baseUrl;
    formModel = this.fb.group({
      Name: ['', Validators.required],
      Surname: ['', Validators.required],
      Email: ['', Validators.email],
      City: ['', Validators.required],
      Admin:['', Validators.required],
      PhoneNumber: ['', [
        Validators.required,
        Validators.pattern("^[0-9]*$"),
        Validators.minLength(9),
        ]
      ],
      Passwords: this.fb.group({
        Password: ['', [
          Validators.required, 
          Validators.pattern(/^(?=\D*\d)(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=.*[!@#$%^&*_=+-]).{8,}$/),
          Validators.minLength(8)
          ]
        ],
        ConfirmPassword: ['', Validators.required]
      }, { validator: this.comparePasswords })
  
    });

    comparePasswords(fb: FormGroup) {
      let confirmPswrdCtrl = fb.get('ConfirmPassword');
      //passwordMismatch
      //confirmPswrdCtrl.errors={passwordMismatch:true}
      if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
        if (fb.get('Password').value != confirmPswrdCtrl.value)
          confirmPswrdCtrl.setErrors({ passwordMismatch: true });
        else
          confirmPswrdCtrl.setErrors(null);
      }
    }

    userAuthentication(data){
        return this._http.post(this._baseUrl + '/api/authenticate/login', data); 
    }

    externalLogin(data){
        return this._http.post(this._baseUrl + '/api/authenticate/social-login', data);
    }

    register(){
      var user = {
        Name: this.formModel.value.Name,
        Surname: this.formModel.value.Surname,
        Email: this.formModel.value.Email,
        Password: this.formModel.value.Passwords.Password,
        City: this.formModel.value.City,
        PhoneNumber: this.formModel.value.PhoneNumber
      };
      return this._http.post(this._baseUrl + '/api/authenticate/register', user);
    }  

    registerAdmin(){
      var admin = {
        Name: this.formModel.value.Name,
        Surname: this.formModel.value.Surname,
        Email: this.formModel.value.Email,
        Password: this.formModel.value.Passwords.Password,
        City: this.formModel.value.City,
        PhoneNumber: this.formModel.value.PhoneNumber
      };
      var role = this.formModel.value.Admin
      return this._http.post(this._baseUrl + '/api/admin/register-admin/' + role, admin);
    }

    confirmation(email: string){
      return this._http.put(this._baseUrl + '/api/authenticate/confirm-email/' + email, { });
    }

    getUserinfo(email){
      return this._http.get(this._baseUrl + '/api/user/user-info/'+ email);
    }

    deleteAccount(email){
      return this._http.delete(this._baseUrl + '/api/user/delete-account/'+ email);
    }

    allAdmins(){
      return this._http.get(this._baseUrl + '/api/admin/all-admins');
    }
}