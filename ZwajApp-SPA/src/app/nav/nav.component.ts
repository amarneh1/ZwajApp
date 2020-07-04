import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { AlertifyService } from '../_Services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService,private router:Router) { }

  ngOnInit() {
  }

  login() {
    //console.log(this.model);
    this.authService.login(this.model).subscribe(
      next => { this.alertify.success('تم الدخول بنجاح') },
      error => { this.alertify.error(error) },
      ()=>{this.router.navigate(['/members']);} // This in IF Its Complete
      //next=>{console.log('تم الدخول بنجاح')},
      // error=>{console.log('فشل في الدخول')}
      //error=>{console.log(error)}
    )
  }

  // loggedIn(){
  //   const token = localStorage.getItem('token');
  //   return !! token
  // }

  loggedIn() {
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem('token');
    this.alertify.message('تم الخروج');
    this.router.navigate(['/home']);
    //console.log('تم الخروج')
  }

}
