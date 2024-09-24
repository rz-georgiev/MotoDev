export interface ClientCarStatusResponse {
repairStatusId: any;
    licensePlateNumber: string;
    vehicleName: string;
    details: ClientCarStatusDetailResponse[];
}

export interface ClientCarStatusDetailResponse {
    statusId: number;
    statusName: string;
    repairName: string;
    repairStartDateTime: string | null;
    repairEndDateTime: string | null;
}