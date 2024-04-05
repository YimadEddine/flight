import { Family } from "./family";
export class Passenger {
    id?:number;
    FullName:string="";
    Type:string="";
    needsDoubleSpace?:boolean;
    Family?:Family;
    PassengerNumber?:number;
}
