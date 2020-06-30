import { Component, OnInit ,Input, Output, EventEmitter} from '@angular/core';
import { AuthService } from '../_Services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //@Input() valuesFromRegister:any; // This Value Come From Home (From Father To Child) So we use @Input
  //We Used EventEmitter As Type
  @Output() cancelRegister= new EventEmitter(); // This Value Will Pass To Home (From Child To Father) So we use @Output
  model: any ={};
  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  register(){
    this.authService.register(this.model).subscribe(
      ()=>{console.log('تم الاشتراك بنجاح')},
      error=>{console.log(error)}
    );
    // console.log('تم الإشتراك');
    // console.log(this.model);
  }

  cancel(){
    console.log('ليس الان');
    this.cancelRegister.emit(false); //By emit we can send the event 
  }

}
