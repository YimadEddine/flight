import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingFormComponent } from './booking-form/booking-form.component';
import { FormsModule } from '@angular/forms';
import { SeatBookingComponent } from './seat-booking/seat-booking.component';
import { CSVbookingComponent } from './csvbooking/csvbooking.component';



@NgModule({
  declarations: [
   BookingFormComponent,
   SeatBookingComponent,
   CSVbookingComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class BookingModule { }
