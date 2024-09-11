import { JwtPayload } from "jwt-decode";

export interface ExtendedJwtPayload extends JwtPayload {
    role: string[];
    userId: number;
    unique_name: string;
    family_name: string;
    nameid: string;
    imageUrl: string;
}