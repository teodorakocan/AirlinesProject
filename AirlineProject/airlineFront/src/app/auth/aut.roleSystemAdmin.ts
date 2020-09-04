import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleSystemAdminGuard implements CanActivate {
  
  constructor(public router: Router) {}
  canActivate(): boolean {
    if (localStorage.getItem('role') != "Admin"){  
      this.router.navigate(['/home']);
      return false;
    }
      return true;
    }
}

