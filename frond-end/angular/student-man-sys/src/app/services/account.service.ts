import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  API_PATH = environment.apiUrl + 'api/';

  requestHeader = new HttpHeaders({ 'No-Auth': 'True' });

  constructor(
    private httpclient: HttpClient,
    private authService: AuthService
  ) { }

  register(registerData: any) {
    return this.httpclient.post(this.API_PATH + 'Account/register', registerData, {
      headers: this.requestHeader,
    });
  }

  public login(loginData: any) {
    return this.httpclient.post(this.API_PATH + 'Account/login', loginData, {
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
}
