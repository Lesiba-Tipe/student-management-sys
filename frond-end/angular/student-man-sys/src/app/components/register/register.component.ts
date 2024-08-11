import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, NgForm, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule, CommonModule, ReactiveFormsModule ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  //registerForm: any;
  registeredSuccessfully: boolean = false;
  formGroupRegister: FormGroup;
  submitted = false;

  constructor(
    private router: Router,
    //private fb: FormBuilder,
    private accountService: AccountService
  ){

    this.formGroupRegister = new FormGroup({
      firstname: new FormControl('', [Validators.required]),
      lastname: new FormControl('', [Validators.required]),
      email:  new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl(),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl('', [Validators.required]),
      
    },
    {
      validators: this.passwordMatchValidator
    });
  }

  public get f(){ return this.formGroupRegister.controls }

  register(){
    this.submitted = true;
    console.log(this.formGroupRegister.value)
    
    if(this.formGroupRegister.valid){
      this.registerUser(this.formGroupRegister);
      
    }else{
      console.log("ERRROR<Register>: ", this.formGroupRegister.status)
    }
    
  }

  registerUser(form: FormGroup){

    this.accountService.register(form.value).subscribe({
      next: (response: any) => {
        if(response){            
          // this.registeredSuccessfully = true;  
          // setTimeout(() => {
          //   this.registeredSuccessfully = false;
          // }, 5000);
          // this.router.navigate(['/login'])
          console.log('Response...', response)
        }
        
      },
      error: (error: any) => {
        console.log('Error from API: ' + error)
        //this.showError()
      },
      complete: () => {
        console.log('Request completed...')
      }
    })
  }

  showError(){

    var displayErrorAlert = document.getElementById('add-error-alert');

    if(displayErrorAlert)
    { 
      displayErrorAlert.style.display = "block"; 
    }

    setTimeout(() => {
      if(displayErrorAlert) { 
        displayErrorAlert.style.display = "none"; 
      }
    }, 5000);
    
}

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if (!password || !confirmPassword) {
      return null; // Return if controls are not yet available
    }

    return password.value === confirmPassword.value ? null : { passwordMismatch: true };
  }


}

