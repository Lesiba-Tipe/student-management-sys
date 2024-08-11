import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  API_PATH = environment.apiUrl + 'Account/';

  requestHeader = new HttpHeaders({ 'No-Auth': 'True' });

  constructor(
    private httpclient: HttpClient,
    private authService: AuthService
  ) { }

  public register(registerData: any): Observable<any> {
    return this.httpclient.post(this.API_PATH + 'register', registerData, {
      headers: this.requestHeader,
    });
  }

  public login(loginData: any) {
    console.log('LOGIN-SERVICE:', loginData)
    return this.httpclient.post(this.API_PATH + 'login', loginData, {
      headers: this.requestHeader,
    });
  }

  public roleMatch(allowedRoles :string[]) : boolean | any{
    let isMatch = false;
    const userRoles: any = this.authService.getRoles();

    if (userRoles != null && userRoles) {
      for (let i = 0; i < userRoles.length; i++) {
        for (let j = 0; j < allowedRoles.length; j++) {
          if (userRoles[i] === allowedRoles[j]) {
            isMatch = true;
            return isMatch;
          } else {
            return isMatch;
          }
        }
      }
    }//else return isMatch;
  }

  public logout(){
    return this.httpclient.delete(this.API_PATH + 'logout',{
      headers: new HttpHeaders({
        'Content-Type': 'json',
        'No-Auth': 'False'
      })
    })
  }

  public getProfile(id: string) {
    return this.httpclient.get(`${this.API_PATH}profile/${id}`);
  }  

  public updateProfile(profile: any){
    return this.httpclient.put(`${this.API_PATH}update/${profile.id}`, profile);
  }
}
