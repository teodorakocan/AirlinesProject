import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Airline } from 'src/app/models';

@Injectable({
  providedIn: 'root'
})

export class AirlineService{

    constructor(private _http: HttpClient, private fb: FormBuilder){}

    private _baseUrl = environment.baseUrl;
    
    airlineNames(): Observable<any>{
      return this._http.get(this._baseUrl + '/api/airline/airlines-name');
    }

    allAirlines(){
      return this._http.get(this._baseUrl + '/api/airline/all-airlines');
    }
}