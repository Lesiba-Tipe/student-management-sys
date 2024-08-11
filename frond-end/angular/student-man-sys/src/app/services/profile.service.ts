import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProfileService  {

  constructor(){}

  public setProfile(profile: {} | any){
    console.log('SET_PROFILE:', profile)
    localStorage.setItem('Profile',JSON.stringify(profile));
  }

  public getProfile(){
    const profile = localStorage.getItem('Profile')
    return profile ? JSON.parse(profile) : null;
  }

  updateProfile(data: any) {
    const jsonProfile = localStorage.getItem('Profile')
    if(jsonProfile){
      const profile = { ...JSON.parse(jsonProfile), ...data };
      console.log('UPDATE_PROFILE:', profile)
      localStorage.setItem('Profile',JSON.stringify(profile));
    }
  }
  
}
