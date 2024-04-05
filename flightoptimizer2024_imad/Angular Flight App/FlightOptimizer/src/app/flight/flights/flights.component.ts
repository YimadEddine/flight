import { Component, OnInit } from '@angular/core';
import { FlightsService } from '../../Services/flights.service';
import { Flight } from '../../Models/flight';
import { response } from 'express';
@Component({
  selector: 'app-flights',
  templateUrl: './flights.component.html',
  styleUrl: './flights.component.css'
})
export class FlightsComponent implements OnInit{
  constructor(private flightService:FlightsService){}
  Flights?:Flight[];
  admin=false;
  ngOnInit(): void {
    this.refreshFlights();
    this.subscribeToReservationSuccess();
  }
  refreshFlights():void{
    this.flightService.GetFlights().subscribe(response=>{
      
      this.Flights=response as Flight[];
      console.log("flights fetched");
    })
  }
  subscribeToReservationSuccess(): void {
    this.flightService.reservationSuccess$.subscribe(() => {
      this.refreshFlights(); // Refresh flight data when reservation is successful
    });
  }

}
