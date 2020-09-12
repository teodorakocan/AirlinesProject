import { Timestamp } from 'rxjs/internal/operators/timestamp';

export class Airline{
    Name: string;
    Address: string;
    PromoDescription: string;
    City: string;
    Satet: string;
}

export class RentService{
    Name: string;
    Address: string;
    PromoDescription: string;
    City: string;
    Satet: string;
}

export class User{
    Name: string;
    Surname: string;
    Email: string;
    Password: string;
    City: string;
    Phone_Number: string;
}

export class Admin{
    Name: string;
    Surname: string;
    Email: string;
    Password: string;
    City: string;
    Phone_Number: string;
}

export class Discounts{
    Points: number;
    Discount: number;
}

export class Branch{
    Name: string;
    Address: string;
    City: string;
    State: string;
    NumberOfVehicle: number;
}

export class Vehicle{
    Brand: string;
    NumberOfSeats: number;
    Class: string;
    Price: number;
    Image: string;
}

export class Destination{
    Name: string;
}

export class Flight{
    StartDateTime: string;
    EndDateTime: string;
    Price: number;
    FlightNumber:string;
    NumberTransfer: number;
    DestinationTransfer: string;
    TravelLenght:string;
}
