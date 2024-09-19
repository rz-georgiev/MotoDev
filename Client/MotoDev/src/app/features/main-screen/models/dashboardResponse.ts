export interface DashboardResponse {
    repairsThisYear: number;
    repairsIncreaseThisYear: number;
    revenueThisMonth: number;
    revenueIncreaseThisMonth: number;
    customersTotal: number;
    customersIncreaseThisYear: number;
    dashboardRecentActivity: DashboardRecentActivity[];
    dashboardReports: DashboardReports;
}

export interface DashboardRecentActivity {
    time: number;
    title: string;
    repairStatusId: number;
}

export interface DashboardReports {
    repairs: number[];
    totalProfits: number[];
    dates: string[];
}