import { Component } from '@angular/core';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { AuthService } from '../../../services/auth.service';
//import { FormsModule,FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormsModule,FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, NgIf } from '@angular/common';
import { Profile } from '../../../data/Profile';


@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [DashboardComponent, FormsModule,CommonModule, NgIf,],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  profile : any// Profile
  submitted = false;

  
  constructor(
    private authService: AuthService,
  )
  {
    
  }

 
  ngOnInit(): void{
    this.profile = this.authService.getProfile()
  }

  updateProfile(): void {
    console.log('Profile:', this.profile)

    //Call Alert
    let type = 'error'
    this.showAlert(type)
  }

  alertMessage : string = '';
  alertType : string = '';
  isSuccess : boolean = false;

  showAlert(type : string): void {
    if (type === 'success') {
      this.isSuccess = true
      this.alertMessage = 'Your profile is successfully updated.';
    } else if (type === 'error') {
      this.alertMessage = 'Your profile could not be updated.';
    }

    var alert = document.getElementById('succ-err-alert');   

    if(alert){ 
      if(this.isSuccess)
        alert.classList.add('bi-check-circle-fill', 'alert-success')
      else
        alert.classList.add('bi-exclamation-triangle-fill', 'alert-danger')

      alert.style.display = "block";           
    }

    setTimeout(() => {
      if(alert) { 
        alert.style.display = "none"; 
      }
    }, 4000);
  }

}
