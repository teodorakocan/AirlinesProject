import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { identifierModuleUrl } from '@angular/compiler';
import { stringify } from 'querystring';

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

    formModel1 = this.fb.group({
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

    formModel2 = this.fb.group({
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

    confirmation(email: string){
      return this._http.put(this._baseUrl + '/api/authenticate/confirm-email/' + email, { });
    }

    adminConfirmation(email: string){
      var newPassword = {
        NewPassword: this.formModel2.value.Passwords.Password,
        ConfirmedPassword: this.formModel2.value.Passwords.ConfirmPassword
      };
      return this._http.put(this._baseUrl + '/api/authenticate/admin-confirmation/' + email, newPassword);
    }
    
    getUserinfo(email){
      return this._http.get(this._baseUrl + '/api/user/user-info/'+ email);
    }

    deleteAccount(email){
      return this._http.delete(this._baseUrl + '/api/user/delete-account/'+ email);
    }
    
    editProfil(email, edit){
      return this._http.put(this._baseUrl + '/api/user/edit-profile/' + email, edit);
    }

    changePassword(email){
      var newPassword = {
        NewPassword: this.formModel2.value.Passwords.Password,
        ConfirmedPassword: this.formModel2.value.Passwords.ConfirmPassword
      };
      return this._http.put(this._baseUrl + '/api/user/change-password/' + email, newPassword, {params:{token:localStorage.getItem('token')}});
    }

    Reservations(email){
      return this._http.get(this._baseUrl + '/api/user/user-reservations/'+ email);
    }

    OldReservations(email){
      return this._http.get(this._baseUrl + '/api/user/old-reservations/'+ email);
    }
    
    serviceForRating(){
      return this._http.get(this._baseUrl + '/api/user/service-for-rating/'+localStorage.getItem('email')); 
    }

    rateService(serviceId, mark)
    {
      return this._http.post(this._baseUrl + '/api/user/rate-service/'+mark+'/'+serviceId+'/'+localStorage.getItem('email'), {}); 
    }
}