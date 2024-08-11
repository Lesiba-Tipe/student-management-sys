import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  
  if (req.headers.get('No-Auth') === 'True') {
    return next(req);
  }
  
  const token = inject(AuthService).getToken();

  const authRequest = req.clone({
    setHeaders: {
      Authorization : `${token}`
    }
  })
  
  return next(authRequest);
};


