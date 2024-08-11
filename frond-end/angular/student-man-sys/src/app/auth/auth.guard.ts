import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  console.log('Guard is working...')

  const authService = inject(AuthService)
  const router = inject(Router)
  const token = authService.getToken()

  if(token){
    return true
  }
  else{
    //Re-Direct to login
    router.navigate(['/login'])
    return false
  }
};
