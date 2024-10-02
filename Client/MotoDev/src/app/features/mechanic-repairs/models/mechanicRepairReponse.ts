import { MechanicRepairResponseDetail } from "./mechanicRepairReponseDetail";

export interface MechanicRepairResponse {
    carImageUrl: string;
    carDescription: string;
    orderName: string;
    details: MechanicRepairResponseDetail[];
}