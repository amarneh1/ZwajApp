import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';

// const httpOptions={
//   headers:new HttpHeaders(
//     {
//       'Authorization':'Bearer ' + localStorage.getItem('token')
//     }
//   )
// }

@Injectable({
  providedIn: 'root'
})
export class UserService {
baseUrl = environment.apiUrl + 'users/'
constructor(private http: HttpClient) { }

getUsers():Observable<User[]>{
  return this.http.get<User[]>(this.baseUrl)
}

getUser(id):Observable<User>{
  return this.http.get<User>(this.baseUrl+ id)
}

}
