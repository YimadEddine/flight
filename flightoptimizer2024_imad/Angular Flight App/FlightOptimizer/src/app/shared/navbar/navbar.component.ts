import { Component } from '@angular/core';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  constructor(private authService:AuthService, private router:Router){}
  Logout():void{
    
    this.authService.Logout();
    if (localStorage.getItem('token') !== null) {
    
    
      
      localStorage.removeItem('token');
      
      
  } 
    this.router.navigate(['/login']);
  }
}
