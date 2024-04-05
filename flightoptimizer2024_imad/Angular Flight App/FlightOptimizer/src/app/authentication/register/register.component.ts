import { Component } from '@angular/core';
import { SignUpModel } from '../../Models/sign-up-model';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
constructor(private authService: AuthService, private router:Router){}
signUpModel:SignUpModel= new SignUpModel();
test?:number;
firstNameErr:string="";
lastNameErr:string="";
emailErr:string="";
phoneNumberErr="";
passwordErr:string="";
emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
phoneRegex = /^\d{10}$/;
passwordRegex =/^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_=+{};:,<.>]).{8,}$/;


Register():void{
  
  if(this.signUpModel.FirstName == "") {this.firstNameErr ="First Name required";return;}
  if(this.signUpModel.LastName=="") {this.lastNameErr="Last Name required";return;}
  if(this.signUpModel.Email=="" || !this.emailRegex.test(this.signUpModel.Email)) {this.emailErr="Enter a valid email";return;}
  if(!this.phoneRegex.test(this.signUpModel.PhoneNumber)) {this.phoneNumberErr="Invalid phone number";return;}
  if(this.signUpModel.Password != this.signUpModel.ConfirmPassword || this.signUpModel.Password=="") {this.passwordErr="Passwords don't match";return;}

  this.authService.createAccount(this.signUpModel).subscribe({
    next: response =>{console.log("res : "+JSON.stringify(response));
      
      if(JSON.stringify(response).includes('Succeeded'))
      {
       
        Swal.fire({
        icon: "success",
     title: "Account created",
     showConfirmButton: false,
      timer: 1500
      })
      this.router.navigate(['/login']);
      }
      else{
       Swal.fire({
          icon: "error",
        title: "An Error has occured while creating your account",
        showConfirmButton: false,
        timer: 1500
         })
       }
    },
     error: err => {console.log(err);}
   });

  

 }

}
