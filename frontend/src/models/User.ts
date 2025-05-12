export enum Gender {
    Male = 0,
    Female = 1
}

export interface User {
    id: number;
    userName: string;
    password: string;
    firstName: string;
    lastName: string;
    gender: Gender;
    email: string;
    imageUrl?: string;
}