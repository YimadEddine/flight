import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Flight } from '../Models/flight';
import { Booking } from '../Models/booking';
import { catchError, throwError} from 'rxjs';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightsService {
  uri = "https://localhost:7184/api/";
  constructor(private http:HttpClient) { }
  reservationSuccess$: Subject<void> = new Subject<void>();
  GetFlights():Observable<Flight[]>
  {
    return this.http.get<Flight[]>(this.uri+"flights");
  }

  GetFlight(id:string|null):Observable<Flight>
  {
    return this.http.get<Flight>(this.uri+"flights/"+id);
  }
  BookFlight(item:Booking):Observable<Booking>{
    alert("in api call");
    return this.http.post<Booking>(this.uri+"bookings", item).pipe(
      catchError((error: any) => {
        
        console.error('An error occurred during booking:', error);
        
        return throwError('Authentication failed'); 
      })
    );;
  }
  
}
