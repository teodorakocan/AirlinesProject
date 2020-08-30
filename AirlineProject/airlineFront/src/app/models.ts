export class Airline{
    name: string;
    address: string;
    promo_Description: string;
}

export class Destination{
    Name: string;
    ImageURL: string;
}

export class Flight{
    Start_DateTime: Date;
    End_DateTime: Date;
    Price: number;
    Price2: number;
    Flight_Number: string;
    Number_Transfer: string;
    Destination_Transfer: string;
    Travel_lenght: number;
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
    Pints: number;
    Discount: number;
}