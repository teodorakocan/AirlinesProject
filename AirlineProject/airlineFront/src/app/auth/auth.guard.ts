import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRoute, RouterStateSnapshot } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate{

    constructor(private router: Router){   }
    canActivate():boolean{
        if(localStorage.getItem('token') != null){
            return true;
        }else{
            this.router.navigate(['/home']);
            return false;
        }
    }
}