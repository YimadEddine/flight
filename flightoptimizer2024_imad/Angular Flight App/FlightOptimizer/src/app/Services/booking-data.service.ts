import { Injectable } from '@angular/core';
import { Family } from '../Models/family';
@Injectable({
  providedIn: 'root'
})
export class BookingDataService {

  FlightId:string|null="";
  Family:Family={};
  constructor() { }

  getFamily():Family{
    return this.Family;
  }
  setFamily(family:Family):void{
    this.Family=family;
  }
  getFlightId():string|null{
    return this.FlightId;
  }
  setFlightId(flightId:string|null):void{
    this.FlightId=flightId;
  }
}
