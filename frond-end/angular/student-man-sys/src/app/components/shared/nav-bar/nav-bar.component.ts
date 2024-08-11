import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { IProfile } from '../../../data/profile';
import { AuthService } from '../../../services/auth.service';
import { ProfileService } from '../../../services/profile.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})

export class NavBarComponent {
  
  profile : IProfile

  constructor(
    private authService: AuthService,
    private router: Router,
    private profileService: ProfileService
  ){
    this.profile = this.profileService.getProfile();
  }

  ngOnInit(): void {
    this.themeSetup()
  }

  public logout() {
    this.authService.clear();
    this.router.navigate(['/login']);
  }

  private themeSetup(): void{
    const theme = {   //default-theme
      primary_color: '#BE1E2D', //Red
      secondary_color: '#fff'   //white
    }

    if(this.profile.dashboard == 'student'){
      theme.primary_color = '#006838'    //Green
      theme.secondary_color = '#42B97A'  //Light-Green
    }
    else if(this.profile.dashboard == 'parent'){
      theme.primary_color = '#662D91'    //Purple
      theme.secondary_color = '#ffff'    //Light-Purple
    }
    else if(this.profile.dashboard == 'admin'){
      theme.primary_color = '#2B3990'    //Blue
      theme.secondary_color = '#ffff'    //Light-Blue
    }
    
    document.documentElement.style.setProperty('--primary-color', theme.primary_color);
    document.documentElement.style.setProperty('--secondary-color', theme.secondary_color);
  }

  initialiseDashboard(){
    
  }
}
