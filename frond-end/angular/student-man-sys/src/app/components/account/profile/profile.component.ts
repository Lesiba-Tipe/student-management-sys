import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { NavBarComponent } from "../../shared/nav-bar/nav-bar.component";
import { RouterLink } from '@angular/router';
import { IProfile } from '../../../data/profile';
import { ProfileService } from '../../../services/profile.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NavBarComponent, RouterLink],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  profile :  IProfile
  
  constructor(
    private authService: AuthService,
    private profileService: ProfileService
  ){ 
    this.profile = this.profileService.getProfile()
    console.log('PROFILE:', this.profile)
   }

}
