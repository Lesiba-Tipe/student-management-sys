import { Component } from '@angular/core';
import { AccountService } from '../../../../services/account.service';
import { FormsModule } from '@angular/forms';
import { CommonModule, NgIf } from '@angular/common';
import { AuthService } from '../../../../services/auth.service';
import { ProfileService } from '../../../../services/profile.service';
import { NavBarComponent } from "../../../shared/nav-bar/nav-bar.component";

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [FormsModule, CommonModule, NgIf, NavBarComponent],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {

  profile : any// Profile
  submitted = false;
  alertMessage : string = '';

  constructor(
    private accountService: AccountService,
    private authService: AuthService,
    private profileService: ProfileService
  ){}
  
  ngOnInit(): void{
    
    this.profile = this.profileService.getProfile()
    
  }
  updateProfile(): void {
    let alert = document.getElementById('succ-err-alert');

    this.accountService.updateProfile(this.profile).subscribe({
      next: (response) => {

        //Update Profile
        this.profileService.updateProfile(response)

        //Update nav-bar

        this.alertMessage = 'Your profile is successfully updated.';
        
        if(alert){
          alert.classList.add('bi-check-circle-fill','alert-success')
          alert.style.display = "block";
        
          setTimeout(() => {
            alert.style.display = "none";
          }, 4000);
        }
      },
      error: (error) => {
        this.alertMessage = 'Your profile could not be updated.';
        
        if(alert){
          alert.classList.add('bi-exclamation-triangle-fill','alert-danger')
          alert.style.display = "block";
        
          setTimeout(() => {
            alert.style.display = "none";
          }, 4000);
        }
      }
    })
  }

}
