import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
// import { error } from 'console';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  // @Input() valuesFromRegister:any;
  @Output () cancelRegister = new EventEmitter() ;
  model : any={};
  constructor(private authservise:AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register(){
    // console.log('تم الإشتراك');
    // console.log(this.model);
    this.authservise.register(this.model).subscribe(
      ()=>{this.alertify.success('تم الاشتراك بنجاح')},
      error=>{this.alertify.error(error)}
    )
  }

  cancel(){
    console.log('ليس الأن');
    this.cancelRegister.emit(false);
  }

}
