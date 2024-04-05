import { Component } from '@angular/core';
import { LoginModel } from '../../Models/login-model';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
import { error } from 'console';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
Account:LoginModel = new LoginModel();
errorMsg:string="";
Error:boolean=false;

constructor(private authService:AuthService, private router:Router){}


 
 
 Authenticate(): void {
  this.authService.Authenticate(this.Account).subscribe(
    (response: any) => {
      if (response.token != "User not found" && response.token != "invalid password") {
        alert(JSON.stringify( response));
        alert('Authentication successful. Token:' + response.token);
        
        localStorage.setItem('token', response.token);
        localStorage.setItem('user',response.user);
        localStorage.setItem('firstName',response.user.firstName);
        localStorage.setItem('lastName',response.user.lastName);
      
        this.router.navigate(['/flights']);
      } else {
        
        
        alert('Authentication failed: ' + response.token);
        this.Error=true;
        this.errorMsg = 'Authentication failed. login or password incorrect';
      }
    },
    (error: any) => {
      console.error('An error occurred during authentication:', error);
      alert('An error occurred during authentication. Please try again later.');
    }
  );
}



}
