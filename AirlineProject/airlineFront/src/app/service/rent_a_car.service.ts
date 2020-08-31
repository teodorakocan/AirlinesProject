import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class RentACarService{

    constructor(private _http: HttpClient, private fb: FormBuilder){}

    private _baseUrl = environment.baseUrl;
    formModel = this.fb.group({
      Name: ['', Validators.required],
      Address: ['', Validators.required],
      Promo_Description: ['', Validators.required]
    });
    
    formModel1 = this.fb.group({
      Name: ['', Validators.required],
      Address: ['', Validators.required],
      Number_Of_Vehicle: ['', Validators.required]
    });

    getMyService(email){
      return this._http.get(this._baseUrl + '/api/serviceadmin/myservice', {params:{email:email}}); 
    }

    allBranches(serviceId){
      return this._http.get(this._baseUrl + '/api/serviceadmin/branches', {params:{serviceId:serviceId}}); 
    }

    addService(){
      var service={
        Name: this.formModel.value.Name,
        Address: this.formModel.value.Address,
        Promo_Description: this.formModel.value.Promo_Description
      }
      return this._http.put(this._baseUrl + '/api/serviceadmin/add-service/' + localStorage.getItem('email'), service); 
    }

    addBranch(serviceId){
      debugger
      var branch={
        Name: this.formModel1.value.Name,
        Address: this.formModel1.value.Address,
        Number_Of_Vehicle: this.formModel1.value.Number_Of_Vehicle
      }
      return this._http.put(this._baseUrl + '/api/serviceadmin/add-branch/' + serviceId, branch);
    }

    addVehicle(branchId, vehicle){
      return this._http.put(this._baseUrl + '/api/serviceadmin/add-vehicle/' + branchId, vehicle);
    }

    allVehicle(){
      return this._http.get(this._baseUrl + '/api/serviceadmin/vehicle/');
    }

    branchVehicle(branchId){
      return this._http.get(this._baseUrl + '/api/serviceadmin/branch-vehicle/'+branchId);
    }

    getBranch(branchId){
      return this._http.get(this._baseUrl + '/api/serviceadmin/branch/' + branchId);
    }

    getVehicle(vehicleId){
      return this._http.get(this._baseUrl + '/api/serviceadmin/vehicle/' + vehicleId);
    }

    editBranch(branchId, edit){
      return this._http.put(this._baseUrl + '/api/serviceadmin/edit-branch/' + branchId, edit);
    }

    editVehicle(vehicleId, edit){
      return this._http.put(this._baseUrl + '/api/serviceadmin/edit-vehicle/' + vehicleId, edit);
    }

    editService(serviceId, edit){
      return this._http.put(this._baseUrl + '/api/serviceadmin/edit-service/' + serviceId, edit);
    }

    deleteBranch(branchId){
      return this._http.delete(this._baseUrl + '/api/serviceadmin/delete-branch/' + branchId);
    }

    deleteService(serviceId){
      return this._http.delete(this._baseUrl + '/api/serviceadmin/delete-service/' + serviceId);
    }

    deleteVehicle(vehicleId){
      return this._http.delete(this._baseUrl + '/api/serviceadmin/delete-vehicle/' + vehicleId);
    }
}