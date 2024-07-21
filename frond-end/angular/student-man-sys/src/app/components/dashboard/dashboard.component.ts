import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { Profile } from '../../data/Profile';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  profile : Profile
  

  constructor(
    private authService: AuthService,
    private router: Router
  ){
    this.profile = this.authService.getProfile();
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
}
