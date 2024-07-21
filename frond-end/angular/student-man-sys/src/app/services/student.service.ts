import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  PATH_OF_API = environment.apiUrl + 'api/';

  requestHeader = new HttpHeaders({ 'No-Auth': 'True' });
  constructor(
    private httpclient: HttpClient,
    private authService: AuthService
  ) {}


  public getUsers(): Observable<any[]> {
    console.log('UserService-getUsers()')
    return this.httpclient.get<any>(this.PATH_OF_API + 'user/users');
  }

  public getUserById(id: string) {
    return this.httpclient.get<any>(this.PATH_OF_API + `user/${id}`);
  }
 
  updateUser(id: number | string, entry: any) {
    return this.httpclient.put(this.PATH_OF_API + `user/${id}`, entry);
  }

  public edit(user: any) {
    return this.httpclient.put(this.PATH_OF_API + `user/update/${user.id}`,user)
  }

  public delete(id: string) {
    return this.httpclient.delete<any>(this.PATH_OF_API + `user/delete/${id}`);
  }
}
