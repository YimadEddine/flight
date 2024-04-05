import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { FlightsComponent } from './flights/flights.component';
import { BrowserModule } from '@angular/platform-browser';
import { BookFlightComponent } from './book-flight/book-flight.component';



@NgModule({
  declarations: [
    HomeComponent,
    FlightsComponent,
    BookFlightComponent
  ],
  imports: [
    CommonModule
  ]
})
export class FlightModule { }
