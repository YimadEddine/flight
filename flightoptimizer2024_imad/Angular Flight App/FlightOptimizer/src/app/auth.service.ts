import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignUpModel } from './Models/sign-up-model';
import { Observable, throwError } from 'rxjs';

import { LoginModel } from './Models/login-model';

import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }
  uri = "https://localhost:7184/api/";

 
  createAccount(newAccount: SignUpModel): Observable<string> {
    return this.http.post<string>(this.uri+"Account", newAccount);
  }


  
  Authenticate(Account: LoginModel): Observable<string> {
    return this.http.post<string>(this.uri + "Account/login", Account).pipe(
      catchError((error: any) => {
        // Handle and log the error
        console.error('An error occurred during authentication:', error);
        // Optionally, you can throw the error to propagate it to the caller
        return throwError('Authentication failed'); // You can customize the error message
      })
    );
  }

  Logout():void{
    if (localStorage.getItem('token') !== null) {
      // Item exists, so remove it
      alert(localStorage.getItem('token'));
      
      localStorage.removeItem('token');
      alert(localStorage.getItem('token'));
      
  } 
  }

}
