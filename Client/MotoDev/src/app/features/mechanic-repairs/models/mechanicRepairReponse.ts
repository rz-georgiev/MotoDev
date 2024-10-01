import { MechanicRepairResponseDetail } from "./mechanicRepairReponseDetail";

export interface MechanicRepairResponse {
    orderName: string;
    details: MechanicRepairResponseDetail[];
}