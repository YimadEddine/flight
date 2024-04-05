import { FlightSeat } from "./flight-seat";
import { Plane } from "./plane";
export class Flight {
    id?:number;
    destination?:string;
    departure?:string;
    availableSeats?:number;
    plane?:Plane ;
    flightSeats?:FlightSeat[];



}
