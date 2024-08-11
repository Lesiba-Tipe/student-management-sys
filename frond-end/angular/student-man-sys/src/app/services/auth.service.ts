import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  public setRoles(roles: []) {
    localStorage.setItem('roles', JSON.stringify(roles));
  }

  public getRoles() {
    return JSON.parse(localStorage.getItem('roles')!);
  }

  public setToken(jwtToken: string) {
    localStorage.setItem('jwtToken', jwtToken);
  }

  public getToken(): string | any {
    return localStorage.getItem('jwtToken');
  }

  // public getId(): string | any {
  //   return localStorage.getItem('Id');
  // }

  // public setId(Id: string) {
  //   localStorage.setItem('Id', Id);
  // }

  public clear() {
    localStorage.clear();
  }

}
