import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {
  model : any = {};

  constructor(public authService : AuthService,private alertify:AlertifyService,private router:Router) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe(
      next=>{this.alertify.success('تم الدخول بنجاح')},
      error=>{this.alertify.error(error)},
      ()=>{this.router.navigate(['/members']);}
    )
    // console.log(this.model);
  }

  loggedIn(){
    return this.authService.loggedIn();
    // const token = localStorage.getItem('token');
    // // if const have value return true else return false
    // return !! token
  }

  loggedOut(){
    localStorage.removeItem('token');
    this.alertify.message('تم الخروج');
    this.router.navigate(['']);
  }

}
