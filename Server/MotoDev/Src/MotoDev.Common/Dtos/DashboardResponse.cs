namespace MotoDev.Common.Dtos
{
    public class DashboardResponse
    {
        public int RepairsThisYear { get; set; }

        public int RepairsIncreaseThisYear { get; set; }

        public int RevenueThisMonth { get; set; }

        public int RevenueIncreaseThisMonth { get; set; }

        public int CustomersTotal { get; set; }

        public int CustomersIncreaseThisYear { get; set; }

        public IEnumerable<DashboardRecentActivity> DashboardRecentActivity { get; set; }

        public DashboardReports DashboardReports { get; set; }
    }

    public class DashboardRecentActivity
    {
        public int Time { get; set; }

        public string Title { get; set; }
    }

    public class DashboardReports
    {
        public IEnumerable<int> Repairs { get; set; }

        public IEnumerable<int> TotalProfits { get; set; }

        public IEnumerable<DateTime> Dates { get; set; }
    }
}