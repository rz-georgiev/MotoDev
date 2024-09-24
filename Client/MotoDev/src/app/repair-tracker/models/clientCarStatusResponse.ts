export interface ClientCarStatusResponse {
    vehicleName: string;
    details: ClientCarStatusDetailResponse[];
}

export interface ClientCarStatusDetailResponse {
    statusId: number;
    statusName: string;
    repairName: string;
    repairStartDateTime: string;
    repairEndDateTime: string | null;
}