import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';
import { TestComponent } from './test/test.component';
import { register } from 'module';
import { FlightsComponent } from './flight/flights/flights.component';
import { ErrorPageComponent } from './shared/error-page/error-page.component';
import { BookingFormComponent } from './booking/booking-form/booking-form.component';
import { SeatBookingComponent } from './booking/seat-booking/seat-booking.component';
import { CSVbookingComponent } from './booking/csvbooking/csvbooking.component';

const routes: Routes = [
  { path: '', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: RegisterComponent },
  { path: 'home', component: TestComponent},
  { path:'flights', component:FlightsComponent},
  { path:'csv', component:CSVbookingComponent},
  { path: 'bookflight/:id', component: BookingFormComponent },
  {path:'BookSeats', component:SeatBookingComponent},
  { path: '**', component: ErrorPageComponent } // Redirect to home if route not found
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
