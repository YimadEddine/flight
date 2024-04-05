import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Passenger } from '../../Models/passenger';
import { Family } from '../../Models/family';
import { BookingDataService } from '../../Services/booking-data.service';
import Swal from 'sweetalert2'
@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrl: './booking-form.component.css'
})
export class BookingFormComponent implements OnInit {

constructor(private route:ActivatedRoute, private bookingData:BookingDataService,
  private router:Router){}
NumberOfPassengers:number=0;
FlightId:string|null="";
passengerType:string="";
Family:Family = { }; 
Passenger:Passenger = new Passenger();
FirstName:string="";
LastName:string="";
RequiresTwoSeats?:boolean=false;
Reserver:Passenger = new Passenger();
firstNameError="";
lastNameError="";
passengerTypeError="";
membersLimitError="";
totalPrice=0;

  ngOnInit(){ 
    
    //this.Reserver.FullName=(localStorage.getItem('firstName') ?? "fistname N/A") + " " + (localStorage.getItem('lastName') ?? "lastname N/A");
    //alert("reserver fullname  "+this.Reserver.FullName);
    this.Family.Members = [];
    //this.Family.Members.push(this.Reserver);
    this.FirstName = localStorage.getItem('firstName') ?? "firstName N/A";
    this.LastName=localStorage.getItem('lastName')?? "lastName N/A";
    this.passengerType = "Adult";
    this.route.paramMap.subscribe({next: (idP)=>{
      this.FlightId = idP.get("id");
    
    }})
    this.bookingData.setFlightId(this.FlightId);
  }
  AddPassenger():void{
    if(this.FirstName === "")
    {
      this.firstNameError="First Name Required";
      return;
    }
    if(this.LastName === "")
    {
      this.lastNameError="Last Name Required";
      return;
    }
    if(this.passengerType === "")
    {
      this.firstNameError="Passenger Type Required";
      return;
    }
    if(this.NumberOfPassengers === 5)
    {
      this.membersLimitError="5 passengers allowed at most";
      Swal.fire({
        icon: "warning",
     title: "Maximum number of seats reached",
     showConfirmButton: false,
      timer: 1500
      })
      return;
    }
    let newPassenger: Passenger = new Passenger();
    newPassenger.FullName=this.FirstName+' '+this.LastName;
    newPassenger.needsDoubleSpace=this.RequiresTwoSeats;
    newPassenger.Type=this.passengerType;
    if(newPassenger.Type=="Child")
    {
      this.totalPrice+=150;
      newPassenger.needsDoubleSpace=false;
    }else if(newPassenger.needsDoubleSpace){this.totalPrice+=500;}
    else{this.totalPrice+=250;}
    this.Family.Members?.push(newPassenger);
    this.NumberOfPassengers+=1;
    newPassenger.PassengerNumber = this.NumberOfPassengers;
    
   

    this.ClearData();
  }
  ClearData():void{
    this.FirstName = "";
    this.LastName="";
    this.firstNameError="";
    this.lastNameError="";
    this.passengerTypeError="";
  }
  RemovePassenger(passengerNumber?:number):void{
 
    for (const passenger of this.Family.Members!) {
      if (passenger.PassengerNumber === passengerNumber) {
        if(passenger.Type =="Adult" && passenger.needsDoubleSpace==false) this.totalPrice-= 250
        else if(passenger.needsDoubleSpace) this.totalPrice-=500
        else this.totalPrice-=150;
        break;
      }
    }
    this.Family.Members=this.Family.Members?.filter(m=>m.PassengerNumber != passengerNumber)
    alert("passenger number : "+passengerNumber);
    this.NumberOfPassengers-=1;
  }
  
  BookSeats()
  {
    this.bookingData.setFamily(this.Family);
    this.router.navigate(['/BookSeats']);
  }

   
  
}
