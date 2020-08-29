import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css']
})
export class ConfirmationComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router,  public service: UserService, private toastr: ToastrService) { }
  public email:string;
  state$;

  ngOnInit(){
    this.state$ = this.route.params.subscribe(params => {
      this.email = params['email'];
    });
  }

  Confirmation(){
      this.service.confirmation(this.email).subscribe(
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
}
