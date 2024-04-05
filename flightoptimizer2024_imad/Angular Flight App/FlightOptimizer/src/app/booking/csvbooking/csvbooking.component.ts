import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BookingService } from '../../Services/booking.service';
import { Flight } from '../../Models/flight';
import { FlightSeat } from '../../Models/flight-seat';
@Component({
  selector: 'app-csvbooking',
  templateUrl: './csvbooking.component.html',
  styleUrl: './csvbooking.component.css'
})
export class CSVbookingComponent {
  selectedFile: File | null = null;
  Flight:Flight={};
  totalworth:number=0;
  loaded=false;
  constructor(private http: HttpClient, private bookingService:BookingService) { }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
  }
  getSeatName(index:number):string|undefined{
    let flightSeat=this.getSeat(index);
    return flightSeat?.seatName;
  }
  getSeat(index: number): FlightSeat {

    let resv = JSON.parse(JSON.stringify(this.Flight.flightSeats?.[index]));

    return  this.Flight.flightSeats!.at(index)! ;
 
  }
  getSeatState(index: number): boolean {

    let resv = JSON.parse(JSON.stringify(this.Flight.flightSeats?.[index]));
    return resv.reserved;
  }
  preventAction(event: MouseEvent, index:number){
    event.preventDefault();
      event.stopPropagation();
  }
  onUpload() {
    
    if (this.selectedFile) {
      this.bookingService.BookCSV(this.selectedFile).subscribe(
        (response) => {
          console.log('File uploaded successfully:',JSON.stringify(response));
          this.Flight = response.flight as Flight;
          this.totalworth = response.totalprice;
          this.loaded=true;

        },
        (error) => {
          console.error('Error uploading file:', error);
        }
      );
    }
  }
}
