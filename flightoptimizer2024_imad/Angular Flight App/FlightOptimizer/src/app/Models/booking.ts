import { Passenger } from "./passenger";
import { Family } from "./family";
import { Flight } from "./flight";
import { FlightSeat } from "./flight-seat";

export class Booking {
    id?:number;
    Reserver?:Passenger;
    Flight?:Flight;
    Family?:Family ;
    Canceled?:boolean=false;
    Seats?:FlightSeat[];
    flightId?:number;
}
