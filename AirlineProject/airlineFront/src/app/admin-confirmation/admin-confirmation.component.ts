import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-confirmation',
  templateUrl: './admin-confirmation.component.html',
  styleUrls: ['./admin-confirmation.component.css']
})
export class AdminConfirmationComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router,  public service: UserService, private toastr: ToastrService) { }
  public email:string;
  fieldTextType: boolean;
  state$;

  ngOnInit() {
    this.state$ = this.route.params.subscribe(params => {
      this.email = params['email'];
    });
  }

  changePassword(){
    this.service.changePassword(this.email).subscribe(
      (res: any) => {
        this.router.navigateByUrl('/signin');
      },
      err=>{
        if(err.status == 400)
        {
            this.toastr.error('You are already confirmed your email.', 'Action not allowed.')
        }
        else
        {
          this.toastr.error('Please create your account.', 'Action not allowed.')
        }
      }
    );
 }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
