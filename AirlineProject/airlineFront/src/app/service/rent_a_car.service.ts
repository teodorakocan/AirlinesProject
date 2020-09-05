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
      City: ['', Validators.required],
      State: ['', Validators.required],
      Address: ['', Validators.required],
      PromoDescription: ['', Validators.required]
    });
    
    formModel1 = this.fb.group({
      Name: ['', Validators.required],
      Address: ['', Validators.required],
      City: ['', Validators.required],
      State: ['', Validators.required],
      NumberOfVehicle: ['', Validators.required]
    });

    serviceNames(): Observable<any>{
      return this._http.get(this._baseUrl + '/api/service/service-names');
    }

    allServices(){
      return this._http.get(this._baseUrl + '/api/service/all-services');
    }

    getMyService(email){
      return this._http.get(this._baseUrl + '/api/service/myservice', {params:{email:email}}); 
    }

    getServiceInfo(name){
      return this._http.get(this._baseUrl + '/api/service/service-info/'+name); 
    }

    allBranches(serviceId){
      return this._http.get(this._baseUrl + '/api/service/branches', {params:{serviceId:serviceId}}); 
    }

    addService(){
      var service={
        Name: this.formModel.value.Name,
        Address: this.formModel.value.Address,
        City: this.formModel.value.City,
        State: this.formModel.value.State,
        PromoDescription: this.formModel.value.PromoDescription
      }
      return this._http.put(this._baseUrl + '/api/service/add-service/' + localStorage.getItem('email'), service); 
    }

    addBranch(serviceId){
      var branch={
        Name: this.formModel1.value.Name,
        Address: this.formModel1.value.Address,
        City: this.formModel1.value.City,
        State: this.formModel1.value.State,
        NumberOfVehicle: this.formModel1.value.NumberOfVehicle
      }
      return this._http.put(this._baseUrl + '/api/service/add-branch/' + serviceId, branch);
    }

    addVehicle(branchId, vehicle){
      return this._http.put(this._baseUrl + '/api/service/add-vehicle/' + branchId, vehicle);
    }

    allVehicle(){
      return this._http.get(this._baseUrl + '/api/service/vehicle');
    }

    serviceVehicle(serviceId){
      return this._http.get(this._baseUrl + '/api/service/service-vehicle/'+serviceId);
    }

    branchVehicle(branchId){
      return this._http.get(this._baseUrl + '/api/service/branch-vehicle/'+branchId);
    }

    getBranch(branchId){
      return this._http.get(this._baseUrl + '/api/service/branch/' + branchId);
    }

    getVehicle(vehicleId){
      return this._http.get(this._baseUrl + '/api/service/vehicle/' + vehicleId);
    }

    editBranch(branchId, edit){
      return this._http.put(this._baseUrl + '/api/service/edit-branch/' + branchId, edit);
    }

    editVehicle(vehicleId, edit){
      return this._http.put(this._baseUrl + '/api/service/edit-vehicle/' + vehicleId, edit);
    }

    editService(serviceId, edit){
      return this._http.put(this._baseUrl + '/api/service/edit-service/' + serviceId, edit);
    }

    deleteBranch(branchId){
      return this._http.delete(this._baseUrl + '/api/service/delete-branch/' + branchId);
    }

    deleteService(serviceId){
      return this._http.delete(this._baseUrl + '/api/service/delete-service/' + serviceId);
    }

    deleteVehicle(vehicleId){
      return this._http.delete(this._baseUrl + '/api/service/delete-vehicle/' + vehicleId);
    }

    findService(searched): Observable<any>{
      //debugger
      return this._http.get(this._baseUrl + '/api/service/search-service?name='+searched.Name+'&city='+searched.City+'&state='+searched.State); 
    }
}