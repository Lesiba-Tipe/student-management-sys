export interface Profile {
    
    id: string;
    dashboard: string
    firstname: string;
    lastname: string;
    email: string;
    idNumber?: string;
    physicalAddress?: string;
    gender?: string;
    studentNo?: string;
    dateOfBirth?: Date;
    grade?: string;
    role: string;
    profilePic: string

}