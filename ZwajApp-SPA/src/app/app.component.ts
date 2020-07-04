
import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './_Services/auth.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  jwtHelper = new JwtHelperService();
  constructor(public authService: AuthService) {
    
  }
  ngOnInit() {
    //If We make Refresh and lost the name of user value which come from login it will be global.
    const token = localStorage.getItem('token');
    this.authService.decodedToken = this.jwtHelper.decodeToken(token);
  }
}
