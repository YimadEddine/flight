import { Flight } from "./flight";
import { Passenger } from "./passenger";

export class FlightSeat {
    id?:number;
    Row?:number;
    Column?:number;
    Reserved?:boolean=false;
    Flight?:Flight;
    seatPassenger?:Passenger;
    seatName?:string;
    SeatName?:string;


}
