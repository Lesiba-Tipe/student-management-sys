import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  var router = inject(Router)
  
  return next(req).pipe(
    catchError(
      (err:HttpErrorResponse) => {
          console.log('Interceptor Error:' + err.status, ':', err);
          if(err.status === 401) {
              router.navigate(['/login']);
          } else if(err.status === 403) {
              router.navigate(['/forbidden']); //Generate this page
          }

          return throwError(()=> err);
      }
  )
  )
};
