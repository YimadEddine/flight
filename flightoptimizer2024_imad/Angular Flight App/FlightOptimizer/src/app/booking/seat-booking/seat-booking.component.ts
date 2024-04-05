import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FlightsService } from '../../Services/flights.service';
import { Router } from '@angular/router';
import { Flight } from '../../Models/flight';
import { Family } from '../../Models/family';
import { FlightSeat } from '../../Models/flight-seat';
import { BookingDataService } from '../../Services/booking-data.service';
import { BookingService } from '../../Services/booking.service';
import { Booking } from '../../Models/booking';
import { Observable, of } from 'rxjs';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-seat-booking',
  templateUrl: './seat-booking.component.html',
  styleUrl: './seat-booking.component.css'
})
export class SeatBookingComponent implements OnInit {
constructor(private flightService:FlightsService, private router:Router, private bookingData:BookingDataService,
  private cdr:ChangeDetectorRef, private bookingService: BookingService){}
divCount = Array(33).fill(0);
Flight:Flight={};
Family:Family={};
FlightId:string|null="";
NumberOfSeats?:number=0;
reservedSeats: string[] = []; 
maxSeatsAllowed: number = 2; 
loaded:boolean=false;



  ngOnInit(): void {
    this.loaded=false;
    this.FlightId=this.bookingData.getFlightId();
    this.Family=this.bookingData.getFamily();
    
    this.NumberOfSeats = this.Family.Members?.length;
    this.flightService.GetFlight(this.FlightId).subscribe(response=>{
      this.Flight = response as Flight;
      this.loaded=true;
    
      let seat = this.Flight.flightSeats![0];
    
      
    })
  
  }
  getSeatState(index: number): boolean {
    
    //console.log("["+index+"] "+JSON.stringify(this.Flight.flightSeats?.[index]));
    let resv = JSON.parse(JSON.stringify(this.Flight.flightSeats?.[index]));
    //console.log("string > parse "+resv.reserved);
    //console.log(false===resv.reserved);
   
    //return  this.Flight.flightSeats!.at(index)! ;
    return resv.reserved;
  }
  getSeat(index: number): FlightSeat {
    
    //console.log("["+index+"] "+JSON.stringify(this.Flight.flightSeats?.[index]));
    let resv = JSON.parse(JSON.stringify(this.Flight.flightSeats?.[index]));
    //console.log("string > parse "+resv.reserved);
   
    return  this.Flight.flightSeats!.at(index)! ;
 
  }
  getSeatName(index:number):string|undefined{
    let flightSeat=this.getSeat(index);
    return flightSeat?.seatName;
  }
  /* SeatIsReserved(index:number):boolean{
    let flightSeat = this.getSeat(index);
    if (flightSeat !== undefined && flightSeat !== null) {
        //console.log("seat : "+index+" "+flightSeat.Reserved);
        return (flightSeat.Reserved!);
    } else {
        return (false);
    }
  } */
  ReserveSeat(event: MouseEvent,seatId: number) {
    
    let flightSeat = this.getSeat(seatId);
    if(this.reservedSeats.length >= this.NumberOfSeats!)
    {
      event.preventDefault();
      event.stopPropagation();
    }

    const alreadyReservedIndex = this.reservedSeats.indexOf(flightSeat?.seatName!);
    
 
    if (alreadyReservedIndex !== -1) {
        this.reservedSeats.splice(alreadyReservedIndex, 1);
        //alert("Seat " + flightSeat?.seatName + " removed from reservation.");
    } else {
 
        if (this.reservedSeats.length < this.NumberOfSeats!) {
            this.reservedSeats.push(flightSeat?.seatName!);
            const index = this.Flight.flightSeats!.findIndex(seat => seat.id === seatId);


        if (index !== -1) {
     
        this.Flight.flightSeats![index].Reserved = true;
        
    } 
            //alert("Seat  " + flightSeat?.seatName + " reserved.  "+this.reservedSeats);
        } else {
          Swal.fire({
            icon: "info",
         title: "Maximum number of seats reached",
         showConfirmButton: false,
          timer: 1500
          })
        }
    }
}


  isSeatReserved(seatId: string): boolean {
    return this.reservedSeats.includes(seatId);
  }
  canReserveSeats(): boolean {
    return this.reservedSeats.length < this.NumberOfSeats!;
  }

  BookFlight()
  {
    
    let booking:Booking = new Booking();
    let flightCopy:Flight = this.Flight;
    flightCopy.flightSeats=[];
    booking.Family=this.Family;
    booking.Flight=this.Flight
    booking.Flight.flightSeats = [];
    booking.flightId=this.Flight.id;
  
    let seats:FlightSeat[]=[];
    for(let i = 0; i<this.reservedSeats.length;i++)
    {
      let FlightSeat:FlightSeat= {SeatName:this.reservedSeats.at(i), Flight:flightCopy};

      seats.push(FlightSeat);
    }
    booking.Seats=seats;
    
    try{
       this.bookingService.BookFlight(booking).subscribe();
       this.router.navigate(['/flights']);
       this.flightService.reservationSuccess$.next();
    }catch(Error){
      this.router.navigate(['/flights']);
      console.log(Error);
    }
   
  }

}
