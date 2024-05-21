import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  // model : any = {};
  registerMode : boolean = false;
  constructor(private http:HttpClient) { }

  ngOnInit() {
  }

  registerToggle(){
    this.registerMode = !this.registerMode ;
    this.registerMode = true;

  }

  cancel() {
    
  }

  // getValues(){
  //   this.http.get('http://localhost:5000/api/values').subscribe(
  //     response => {this.values = response;},
  //     error => {console.log(error);}
  //   )
  // }

  cancelRegister(mode:boolean){
    this.registerMode = mode;
  }

}
