import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Booking } from '../Models/booking';
import { Observable } from 'rxjs';
import { catchError, throwError } from 'rxjs';
import { LoginModel } from '../Models/login-model';
@Injectable({
  providedIn: 'root'
})
export class BookingService {
  uri="https://localhost:7184/api/";
  constructor(private http:HttpClient) { }

  BookCSV(file:File){
 
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<any>(this.uri+"bookings/csv", formData).pipe(
      catchError((error: any) => {
      
        console.error('An error occurred during upload:', error);
      
        return throwError('upload failed'); 
      })
    );
  }

  BookFlight(item:Booking):Observable<Booking>{
    
    return this.http.post<Booking>(this.uri+"bookings", item).pipe(
      catchError((error: any) => {
      
        console.error('An error occurred during booking:', error);
      
        return throwError('Authentication failed'); 
      })
    );
  }
  

}
