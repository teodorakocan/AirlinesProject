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

export class SystemAdminService{

    constructor(private _http: HttpClient, private fb: FormBuilder){}
  
    private _baseUrl = environment.baseUrl;
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
    formModel3 = this.fb.group({
        Points: ['',Validators.required],
        Discount: ['',Validators.required]
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

    registerAdmin(){
        var admin = {
          Name: this.formModel1.value.Name,
          Surname: this.formModel1.value.Surname,
          Email: this.formModel1.value.Email,
          Password: this.formModel1.value.Passwords.Password,
          City: this.formModel1.value.City,
          PhoneNumber: this.formModel1.value.PhoneNumber
        };
        var role = this.formModel1.value.Admin
        debugger
        return this._http.post(this._baseUrl + '/api/systemadmin/register-admin/' + role, admin);
    }

    allDiscounts()
    {
      return this._http.get(this._baseUrl + '/api/systemadmin/discounts');
    }
    
    editDiscount(discount, id){
      return this._http.put(this._baseUrl + '/api/systemadmin/edit-discount/' + id, discount);
    }

    addDiscount()
    {
      var discount = {
        Points: this.formModel3.value.Points,
        Discount: this.formModel3.value.Discount
      };
      return this._http.post(this._baseUrl + '/api/systemadmin/add-discount', discount);
    }

    allUsers(){
      return this._http.get(this._baseUrl + '/api/systemadmin/all-users');
    }

    allCompanies(){
      return this._http.get(this._baseUrl + '/api/systemadmin/all-companies');
    }
}