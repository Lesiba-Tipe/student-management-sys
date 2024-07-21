import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { RouterLink} from '@angular/router';
import { Profile } from '../../data/Profile';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  invalidLogin: boolean = false;

  profile : Profile = {
    id: 'null-id',
    email: 'student@evasity.ac.za',
    dashboard: 'parent',
    firstname: 'Jane',
    lastname: 'Doe',
    role: 'parent',
    dateOfBirth: new Date(2009,3,5),
    idNumber: '090305-7593-208',
    studentNo: '35612896',
    gender: 'Male',
    grade: '9',
    profilePic: 'https://drive.google.com/file/d/1cDN2LV1TTTntYSc_IqQ1QITX_fPsuhSE/'
  }



  constructor(
    private accountService: AccountService, 
    private authService: AuthService,
    private router: Router,
  ){

    authService.setProfile(this.profile);
  }

  login(loginForms: NgForm) {

    this.accountService.login(loginForms.value).subscribe(
      (response: any) => {
        this.onSuccess(response)        
      },
      (error) => {   
      
        if(error.status === 0)
        {
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
      }
    );
  }

 
  onSuccess(response: any){
        this.invalidLogin = false;

        this.authService.setToken(response.jwtToken);
        console.log(response.jwtToken)

        this.authService.setRoles(response.roles);
        this.authService.setId(response.id);

        const role = response.roles[0];
        console.log('ROLE:', role)

        //Initialize user
        //this.initilize_Profile()

        //this.themeSetup()
        this.router.navigate(['/dashboard']);       
  }



}
