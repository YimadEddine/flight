import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd} from '@angular/router';
import { filter } from 'rxjs/operators';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  showNavbar: boolean = true;

  constructor(private router: Router) {}

  ngOnInit() {
   
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
   
      if (event.url === '/login' || event.url === '/signup') {
        this.showNavbar = false; 
      } else {
        this.showNavbar = true; 
      }
    });
  }
}