import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { RouterLink} from '@angular/router';
import { NgIf } from '@angular/common';
import { IProfile } from '../../data/profile';
import { ProfileService } from '../../services/profile.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, FormsModule,NgIf],
  providers: [
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {

  invalidLogin: boolean = false;

  constructor(
    private accountService: AccountService, 
    private authService: AuthService,
    private router: Router,
    private profileService: ProfileService
  ){
    this.authService.clear();
  }

  login(loginForm: NgForm) {

    this.accountService.login(loginForm.value).subscribe({
      next: (response: any) => {
        if(response){
          this.onLoginSuccess(response)        
        }
      },
      error: (error) => {   
      
        if(error.status === 0)
        {
          console.log('STATUS : ', error.status)
          console.log('ERROR: ', error)
          
          alert('Server is not available, Please try again later');
        }
        else{
          var displayErrorAlert = document.getElementById('login-error-alert');   

          if(displayErrorAlert){ 
            displayErrorAlert.style.display = "block";           
          }

          setTimeout(() => {
            if(displayErrorAlert) { 
              displayErrorAlert.style.display = "none"; 
            }
          }, 5000);

          this.invalidLogin = true; 

          console.log('STATUS: ', error.status)
          console.log('ERROR: ', error)
        }
      },

    });

  }

  onLoginSuccess(response: any){   //Set || Save user data [id, roles, token and profile]
    this.invalidLogin = false;

    this.authService.setToken(response.jwtToken);
    var profile : IProfile //{ [key: string]: any } = {};
    console.log('TOKEN:', response.jwtToken)
    //Get Profile
    this.accountService.getProfile(response.id).subscribe({
      next: (response: any) => 
        { 
          profile = response; 
          profile.dashboard = 'admin'; //response.roles[0]
          profile.role = 'admin'; //response.roles[0]
          
          this.authService.setRoles(response.roles)
          this.profileService.setProfile(profile)

          this.router.navigate(['/dashboard'])
        },
      error: (error) => {
        this.router.navigate(['/login']) //change to 404 not Found
      } 
    });

  }
}
