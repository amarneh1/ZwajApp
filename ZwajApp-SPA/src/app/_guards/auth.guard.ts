import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../_Services/auth.service';
import { AlertifyService } from '../_Services/alertify.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authservice: AuthService, private router: Router, private alertfy: AlertifyService) {

  }

  canActivate(): boolean {
    if (this.authservice.loggedIn()) {
      return true;
    }
    this.alertfy.error('يجب تسجيل الدخول اولاً');
    this.router.navigate(['']);
    return false;
  }
}
